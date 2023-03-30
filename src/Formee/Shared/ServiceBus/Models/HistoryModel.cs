namespace ServiceBus.Models;

public class HistoryModel
{
    public string? Title { get; set; }

    public string? Action { get; set; }

    public Guid UserId { get; set; }

    public string? Service { get; set; } = "Analytics";
}
