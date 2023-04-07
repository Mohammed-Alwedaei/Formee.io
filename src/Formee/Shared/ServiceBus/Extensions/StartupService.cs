using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceBus.ServiceBus;

namespace ServiceBus.Extensions;

public class StartupService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public StartupService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var historyServiceBus = scope.ServiceProvider
            .GetRequiredService<IAzureServiceBus<HistoryServiceBus>>();

        var notificationServiceBus = scope.ServiceProvider
            .GetRequiredService<IAzureServiceBus<NotificationsServiceBus>>();

        await historyServiceBus.StartProcessing();
        await notificationServiceBus.StartProcessing();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var historyServiceBus = scope.ServiceProvider
            .GetRequiredService<IAzureServiceBus<HistoryServiceBus>>();

        var notificationServiceBus = scope.ServiceProvider
            .GetRequiredService<IAzureServiceBus<NotificationsServiceBus>>();

        await historyServiceBus.StartProcessing();
        await notificationServiceBus.StartProcessing();
    }
}
