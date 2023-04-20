namespace Client.Web.Utilities.Dtos;

public class NotificationDto
{
    public int Id { get; set; }

    public Guid GlobalUserId { get; set; }

    public string? Heading { get; set; }

    public string? Message { get; set; }

    public bool IsRead { get; set; }

    public DateTime CreatedDate { get; set; }
}
