using Identity.BusinessLogic.Contexts;
using Identity.BusinessLogic.Models;
using Identity.BusinessLogic.Services;
using Identity.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SynchronousCommunication.Extensions;

namespace Identity.API.Extensions;

public static class ServicesConfiguration
{
    public static IServiceCollection AddOpenApi
        (this IServiceCollection services)
    {
        // Add services to the container.
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IServiceCollection AddConfiguration
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BlobStorageConfiguration>(
            configuration.GetSection("AzureStorage"));

        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("DefaultConnection"));

        services.AddScoped<IIdentityManager, IdentityManager>();

        services.AddHttpClient<IIdentityManager, IdentityManager>(options =>
            options.BaseAddress = new
                Uri(configuration.GetValue<string>("Auth0:APIUrl")));

        return services;
    }

    public static IServiceCollection AddApplicationDatabase
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddSyncCommunication();

        return services;
    }

    public static IServiceCollection AddIdentityManagement
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, JwtBearerDefaults.AuthenticationScheme, c =>
            {
                c.Authority = $"https://{configuration["Auth0:Domain"]}";
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = configuration["Auth0:Audience"],
                    ValidIssuer = $"https://{configuration["Auth0:Domain"]}"
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("users", policy =>
            {
                policy.RequireClaim("scope", "user:read");
            });
        });


        return services;
    }
}
