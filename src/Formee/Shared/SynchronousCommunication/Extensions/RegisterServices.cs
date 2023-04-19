using Microsoft.Extensions.DependencyInjection;
using SynchronousCommunication.HttpClients;

namespace SynchronousCommunication.Extensions;

public static class RegisterServices
{
    public static IServiceCollection AddSyncCommunication(this IServiceCollection services)
    {
        services.AddHttpClient<ISubscriptionsClient, SubscriptionsClient>();

        return services;
    }
}