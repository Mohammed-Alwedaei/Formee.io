using Newtonsoft.Json;

namespace ServiceBus.Models;

public class NotificationsServiceBusConnection
{
    public string ConnectionString { get; set; }

    [JsonProperty("Notifications")]
    public string Topic { get; set; }
    
    [JsonProperty("NotificationsSubscription")]
    public string Subscription { get; set; }

}
