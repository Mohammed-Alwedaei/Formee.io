using ServiceBus.Models;

namespace ServiceBus.Messages;

public class NotificationMessage : BaseServiceBusMessage
{
    public NotificationModel Entity { get; set; }

    public static implicit operator NotificationMessage(NotificationModel notification)
    {
        return new NotificationMessage
        {
            Entity = notification,
        };
    }
}
