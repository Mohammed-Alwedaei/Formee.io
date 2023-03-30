using Microsoft.Extensions.DependencyInjection;
using ServiceBus.ServiceBus;

namespace ServiceBus.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddAzureServiceBus
        (this IServiceCollection services)
    {
        services.AddScoped<IAzureServiceBus, AzureServiceBus>();

        return services;
    }
}
