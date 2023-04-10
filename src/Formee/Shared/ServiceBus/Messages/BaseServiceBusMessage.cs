namespace ServiceBus.Messages;

public class BaseServiceBusMessage
{
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
