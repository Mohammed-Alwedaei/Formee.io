using Identity.BusinessLogic.Contexts;
using Identity.BusinessLogic.Models;
using Identity.BusinessLogic.Services;
using Identity.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SynchronousCommunication.Extensions;
using System.Text;
using Microsoft.OpenApi.Models;
using ServiceBus;

namespace Identity.API.Extensions;

public static class ServicesConfiguration
{
    public static IServiceCollection AddOpenApi
        (this IServiceCollection services)
    {
        // Add services to the container.
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mango.Services.CouponAPI", Version = "v1" });
            //c.EnableAnnotations();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"Enter 'Bearer' [space] and your token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        },
                        Scheme="oauth2",
                        Name="Bearer",
                        In=ParameterLocation.Header
                    },
                    new List<string>()
                }

            });
        });

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
        services.AddScoped<IJwtManager, JwtManager>();

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
        services.AddServiceBusSender();

        return services;
    }

    public static IServiceCollection AddIdentityManagement
        (this IServiceCollection services, IConfiguration configuration)
    {
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
}
