using Newtonsoft.Json;

namespace Infrastructure.ServiceBus;

public class NotificationsServiceBusConnection
{
    public string? ConnectionString { get; set; }

    public string? NotificationsTopic { get; set; }

    public string? NotificationsSubscription { get; set; }

}
