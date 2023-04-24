using System.Text;
using HealthChecks.SqlServer;
using Links.API.Middlewares;
using Links.BusinessLogic.Contexts;
using Links.BusinessLogic.Repositories;
using Links.BusinessLogic.Repositories.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SynchronousCommunication.Extensions;

namespace Links.API.Extensions;

public static class RegisterServices
{
    private static IConfiguration? _configuration;
    
    /// <summary>
    /// Register Open API (Swagger Docs) in the dependency injection system
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }
    
    /// <summary>
    /// Register Links service health check in the dependency injection system
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddHealthMonitoring(this IServiceCollection services)
    {
        var sqlServerConnectionString = _configuration?.GetConnectionString("DefaultConnection");

        services.AddHealthChecks()
            .AddSqlServer(sqlServerConnectionString);

        return services;
    }

    /// <summary>
    /// Register persistant services 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddPersistant(this IServiceCollection services)
    { 
        services.AddSingleton<DbContext>();

        services.AddSyncCommunication();
        services.AddScoped<ILinkRepository, LinkRepository>();
        services.AddScoped<ILinkHitRepository, LinkHitRepository>();

        return services;
    }

    /// <summary>
    /// Add global exception handling
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddTransient<GlobalExceptionHandlerMiddleware>();

        return services;
    }

    public static IServiceCollection AddIdentityManagement(this IServiceCollection services)
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