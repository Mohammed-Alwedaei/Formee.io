using Infrastructure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Extensions;

public class StartupService : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public StartupService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var serviceBusConsumer = scope.ServiceProvider
            .GetRequiredService<NotificationsServiceBus>();

        await serviceBusConsumer.StartProcessing();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var serviceBusConsumer = scope.ServiceProvider
            .GetRequiredService<NotificationsServiceBus>();

        await serviceBusConsumer.StopProcessing();
    }
}