using Microsoft.Extensions.DependencyInjection;
using ServiceBus.Messages;
using ServiceBus.ServiceBus;

namespace ServiceBus;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceBusSender(this IServiceCollection services)
    {
        services.AddScoped<IAzureServiceBus<HistoryMessage>, HistoryServiceBus>();
        services.AddScoped<IAzureServiceBus<NotificationMessage>, NotificationServiceBus>();

        return services;
    }
}
