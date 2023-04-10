using Newtonsoft.Json;

namespace ServiceBus.Models;

public class HistoryServiceBusConnection
{
    public string? ConnectionString { get; set; }

    [JsonProperty("History")]
    public string? Topic { get; set; }

    [JsonProperty("HistorySubscription")]
    public string? Subscription { get; set; }
}
