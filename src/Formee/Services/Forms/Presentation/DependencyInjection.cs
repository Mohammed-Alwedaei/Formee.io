using System.Text;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Presentation.Middlewares;

namespace Presentation;

/// <summary>
/// DependencyInjection class registers all presentation layer dependencies 
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Register Open API service (Swagger)
    /// </summary>
    /// <param name="services"></param>
    /// <returns>The IServiceCollection</returns>
    public static IServiceCollection AddOpenApi(
        this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    /// <summary>
    /// Register the authentication and authorization services
    /// </summary>
    /// <param name="services"></param>
    /// <returns>The IServiceCollection</returns>
    public static IServiceCollection AddIdentityManagement(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("DefaultConnection"));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, JwtBearerDefaults.AuthenticationScheme, c =>
            {
                c.Authority = $"https://{configuration["Identity:Issuer"]}";
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = configuration["Identity:Audience"],
                    ValidIssuer = "dev-pnxnfhh8.us.auth0.com",
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes
                        (configuration["Identity:SecretKey"]))
                };
            });
        services.AddAuthorization();

        return services;
    }

    /// <summary>
    /// Use the authentication and authorization services in the pipeline
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication UseIdentityManagement(
        this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandler>();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHealthChecks("/healthcheck", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }
}
