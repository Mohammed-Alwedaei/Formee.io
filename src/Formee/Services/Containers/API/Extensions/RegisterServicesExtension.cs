using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class RegisterServicesExtension
{
    private static IConfiguration? _configuration;
    
    /// <summary>
    /// Register Open API (Swagger Docs) in the dependency injection system
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
    
    /// <summary>
    /// Register Analytics service health check in the dependency injection system
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddMonitoring(this IServiceCollection services)
    {
        var mongoDbConnectionString = _configuration?.GetValue<string>("ContainersDatabase:ConnectionString");
        var serviceBusConnectionString = _configuration?.GetValue<string>("ServiceBus:ConnectionString");
        var serviceBusHistoryTopic = _configuration?.GetValue<string>("ServiceBus:HistoryTopic");

        services.AddHealthChecks()
            .AddMongoDb(mongoDbConnectionString)
            .AddAzureServiceBusTopic(serviceBusConnectionString, serviceBusHistoryTopic);

        return services;
    }
    
    /// <summary>
    /// Register persistant services 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddPersistant(this IServiceCollection services)
    {
        services.AddSingleton<ContainersService>();
        
        services.AddAutoMapper(typeof(Program));

        services.AddServiceBusSender();

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
        
        services.Configure<ContainersDatabaseConfiguration>(
            configuration.GetSection("ContainersDatabase"));

        return services;
    }
}