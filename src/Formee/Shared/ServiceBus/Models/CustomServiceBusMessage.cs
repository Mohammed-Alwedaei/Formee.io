namespace ServiceBus.Models;

public class CustomServiceBusMessage
{
    public HistoryModel? History { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
