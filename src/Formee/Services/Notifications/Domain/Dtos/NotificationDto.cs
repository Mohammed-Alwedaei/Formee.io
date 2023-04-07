namespace Domain.Dtos;

public class NotificationDto : Dto
{
    [Required]
    public Guid GlobalUserId { get; set; }

    public string? Heading { get; set; }

    public string? Message { get; set; }

    public bool IsRead { get; set; }

    public static implicit operator NotificationDto
        (NotificationEntity notification)
    {
        return new NotificationDto
        {
            Id = notification.Id,
            GlobalUserId = notification.GlobalUserId,
            Heading = notification.Heading,
            Message = notification.Message,
            IsRead = notification.IsRead,
            CreatedDate = notification.CreatedDate,
        };
    }
}
