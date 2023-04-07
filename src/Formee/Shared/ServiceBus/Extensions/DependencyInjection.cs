using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBus.Models;
using ServiceBus.ServiceBus;

namespace ServiceBus.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddNotificationServiceBus
        (this IServiceCollection services)
    {
        services.AddScoped<IAzureServiceBus<NotificationsServiceBus>,
            NotificationsServiceBus>();

        return services;
    }

    public static IServiceCollection AddHistoryServiceBus
        (this IServiceCollection services)
    {
        services.AddScoped<IAzureServiceBus<HistoryServiceBus>, HistoryServiceBus>();

        return services;
    }

    public static IServiceCollection AddBackgroundProcessingTask(this IServiceCollection services)
    {
        services.AddHostedService<StartupService>();

        return services;
    }
    
    public static IServiceCollection AddConfiguration(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<NotificationsServiceBusConnection>(
            configuration.GetSection("ServiceBus"));

        services.Configure<HistoryServiceBusConnection>(
            configuration.GetSection("ServiceBus"));

        return services;
    }

}
