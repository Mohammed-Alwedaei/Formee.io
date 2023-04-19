using Identity.BusinessLogic.Contexts;
using Identity.BusinessLogic.Models;
using Identity.BusinessLogic.Services;
using Identity.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
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

        services.AddScoped<IIdentityManager, IdentityManager>();

        services.AddHttpClient<IIdentityManager, IdentityManager>(options =>
            options.BaseAddress = new
                Uri(configuration.GetValue<string>("Identity:APIUrl")));

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
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = 
                JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = 
                JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = configuration.
                GetValue<string>("Identity:Authority");

            options.Audience = configuration
                .GetValue<string>("Identity:Audience");
        });

        services.AddAuthorization();

        return services;
    }
}
