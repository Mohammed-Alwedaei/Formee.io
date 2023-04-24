using Domain.Interfaces;
using HealthChecks.UI.Client;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Infrastructure.ServiceBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBus;

namespace Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddHealthChecks()
            .AddSqlServer(connectionString);

        services.Configure<NotificationsServiceBusConnection>(
            configuration.GetSection("ServiceBus"));

        services.AddScoped<IEmailsManager, EmailsManager>();
        services.AddScoped<INotificationsManager, NotificationsManager>();

        services.AddSingleton<NotificationsServiceBus>();

        services.AddHostedService<StartupService>();

        return services;
    }

    public static WebApplication UseHealthMonitoring(this WebApplication app)
    {
        app.MapHealthChecks("/healthcheck", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }
}
