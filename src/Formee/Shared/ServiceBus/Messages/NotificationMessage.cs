using ServiceBus.Models;

namespace ServiceBus.Messages;

public class NotificationMessage : BaseServiceBusMessage
{
    public NotificationModel Entity { get; set; }
}
