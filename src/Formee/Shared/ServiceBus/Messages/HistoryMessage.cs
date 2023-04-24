using ServiceBus.Models;

namespace ServiceBus.Messages;

public class HistoryMessage : BaseServiceBusMessage
{
    public object Entity { get; set; }

    public static implicit operator HistoryMessage(HistoryModel history)
    {
        return new HistoryMessage
        {
            Entity = history,
        };
    }
}
