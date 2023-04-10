namespace ServiceBus.ServiceBus;

public interface IAzureServiceBus<in TMessage> where TMessage : class
{
    Task SendMessage(TMessage body);
}
