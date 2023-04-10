namespace ServiceBus.Models;

public class NotificationModel
{
    public Guid GlobalUserId { get; set; }

    public string? Heading { get; set; }

    public string? Message { get; set; }

    public bool IsRead { get; set; }
}
