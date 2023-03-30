namespace Analytics.BusinessLogic.ServiceBus;

public interface IServiceBus
{
    Task Start();

    Task Stop();
}
