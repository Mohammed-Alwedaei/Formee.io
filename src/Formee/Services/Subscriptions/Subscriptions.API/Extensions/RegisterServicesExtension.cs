using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Subscriptions.BusinessLogic;
using Subscriptions.BusinessLogic.DbContexts;
using Subscriptions.BusinessLogic.Repositories;
using Subscriptions.BusinessLogic.Repositories.IRepository;

namespace Subscriptions.API.Extensions;

public static class RegisterServicesExtension
{
    private static IConfiguration? _configuration;
    
    /// <summary>
    /// Register Open API (Swagger Docs) in the dependency injection system
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddEndpointsAndDocumentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingConfiguration));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICouponRepository, CouponRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<ISubscriptionFeatureRepository, SubscriptionFeatureRepository>();

        return services;
    }
    
    /// <summary>
    /// Register Analytics service health check in the dependency injection system
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddObservability(this IServiceCollection services)
    {
        var sqlServerConnectionString = _configuration?.GetConnectionString("DefaultConnection");

        services.AddHealthChecks()
            .AddSqlServer(sqlServerConnectionString);

        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddPersistent(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
        
        return services;
    }
    
    /// <summary>
    /// Register Service security (Authentication, Authorization, CORS)
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddIdentityAndSecurity(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, JwtBearerDefaults.AuthenticationScheme, c =>
            {
                c.Authority = $"https://{_configuration["Auth0:Domain"]}";
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = _configuration["Auth0:Audience"],
                    ValidIssuer = $"https://{_configuration["Auth0:Domain"]}"
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
    public static IServiceCollection AddConfiguration
        (this IServiceCollection services, IConfiguration? configuration)
    {
        _configuration = configuration;

        return services;
    }
}