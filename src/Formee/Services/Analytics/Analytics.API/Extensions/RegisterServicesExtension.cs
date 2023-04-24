using System.Text;
using System.Text.Json.Serialization;
using Analytics.BusinessLogic.Contexts;
using Analytics.BusinessLogic.Mapper;
using Analytics.BusinessLogic.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceBus;
using SynchronousCommunication.Extensions;

namespace Analytics.API.Extensions;

public static class ServicesExtensions
{
    private static IConfiguration? _configuration;

    public static IServiceCollection AddServiceEndpoints(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                o.JsonSerializerOptions.MaxDepth = 0;
            });

        return services;
    }

    /// <summary>
    /// Register Analytics service health check in the dependency injection system
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddServiceHealthChecks(this IServiceCollection services)
    {
        var sqlServerConnectionString = _configuration?.GetConnectionString("DefaultConnection");
        var serviceBusConnectionString = _configuration?.GetValue<string>("ServiceBus:ConnectionString");
        var serviceBusHistoryTopic = _configuration?.GetValue<string>("ServiceBus:HistoryTopic");

        services.AddHealthChecks()
            .AddSqlServer(sqlServerConnectionString)
            .AddAzureServiceBusTopic(serviceBusConnectionString, serviceBusHistoryTopic);

        return services;
    }

    /// <summary>
    /// Register Open API (Swagger Docs) in the dependency injection system
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddServiceDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    /// <summary>
    /// Register persistant services 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddServicePersistant(this IServiceCollection services)
    {
        var sqlServerConnectionString = _configuration?.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationDbContext>(options => options
            .UseSqlServer(sqlServerConnectionString));

        services.AddServiceBusSender();

        return services;
    }

    /// <summary>
    /// Register domain Dependencies and services
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingConfiguration));
        
        services.AddScoped<ISiteRepository, SiteRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IPageHitRepository, PageHitRepository>();
        services.AddScoped<ISessionRepository, SessionsRepository>();
        
        services.AddServiceBusSender();

        services.AddSyncCommunication();
        
        return services;
    }
    
    /// <summary>
    /// Register Service security (Authentication, Authorization, CORS)
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddServiceSecurity(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, JwtBearerDefaults.AuthenticationScheme, c =>
            {
                c.Authority = $"https://{_configuration["Identity:Issuer"]}";
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = _configuration["Identity:Audience"],
                    ValidIssuer = "dev-pnxnfhh8.us.auth0.com",
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes
                        (_configuration["Identity:SecretKey"]))
                };
            });

        services.AddAuthorization();

        services.AddCors(options =>
        {
            options.AddPolicy("cors", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }

    /// <summary>
    /// Add configuration to be used to access service configurations (AppSettings)
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddServiceConfiguration
        (this IServiceCollection services, IConfiguration? configuration)
    {
        _configuration = configuration;

        return services;
    }
}