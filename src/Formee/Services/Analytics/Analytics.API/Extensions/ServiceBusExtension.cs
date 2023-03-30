using Analytics.BusinessLogic.ServiceBus;

namespace Analytics.API.Extensions;

public static class ServiceBusExtension
{
    public static IServiceBus ServiceBus { get; private set; }

    public static IApplicationBuilder UseServiceBusConsumer
        (this IApplicationBuilder app)
    {
        ServiceBus = app.ApplicationServices.GetService<IServiceBus>();

        var hostApplicationLifetime = app.ApplicationServices
            .GetService<IHostApplicationLifetime>();

        hostApplicationLifetime.ApplicationStarted.Register(OnStart);
        hostApplicationLifetime.ApplicationStarted.Register(OnStop);

        return app;
    }

    private static void OnStart()
    {
        ServiceBus.Start();
    }

    private static void OnStop()
    {
        ServiceBus.Stop();
    }
}
