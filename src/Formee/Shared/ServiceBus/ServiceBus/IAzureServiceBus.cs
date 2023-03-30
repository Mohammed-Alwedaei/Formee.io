namespace ServiceBus.ServiceBus;

public interface IAzureServiceBus
{
    Task SendMessage<TBody>(TBody body);
}
