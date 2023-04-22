using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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
        this IServiceCollection services, IConfiguration config)
    {
        services.AddHealthChecks()
            .AddSqlServer(config.GetConnectionString("DefaultConnection"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = config["Auth0:Authority"];
            options.Audience = config["Auth0:Audience"];
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("users", policy =>
            {
                policy.RequireClaim("user:read");
            });
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("IsUser", policy =>
            {
                policy.RequireClaim("role", "user");
            });
        });

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
