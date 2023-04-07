namespace ServiceBus.ServiceBus;

public interface IAzureServiceBus<TService> where TService : class
{
    Task SendMessage<TBody>(TBody body);

    Task StartProcessing();

    Task StopProcessing();
}
