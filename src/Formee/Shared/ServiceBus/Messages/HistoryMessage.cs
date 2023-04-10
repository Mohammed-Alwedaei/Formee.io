namespace ServiceBus.Messages;

public class HistoryMessage : BaseServiceBusMessage
{
    public object Entity { get; set; }
}
