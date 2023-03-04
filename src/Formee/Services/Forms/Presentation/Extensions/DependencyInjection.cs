﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Presentation.Middlewares;

namespace Presentation.Extensions;

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
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = 
                    JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme = 
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = config
                    .GetSection("IdentityManagement")
                    .GetValue<string>("Authority"); ;

                options.Audience = config
                    .GetSection("IdentityManagement")
                    .GetValue<string>("Audience"); ;
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

        return app;
    }
}