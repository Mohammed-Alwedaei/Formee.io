namespace Domain.Entities;

public class NotificationEntity : Entity
{
    [Required]
    public Guid GlobalUserId { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(30)]
    public string Heading { get; set; } = null!;

    [Required]
    [MinLength(10)]
    [MaxLength(255)]
    public string Message { get; set; } = null!;

    [Required]
    public bool IsRead { get; set; } = false;

    public static implicit operator NotificationEntity
        (NotificationDto notificationDto)
    {
        return new NotificationEntity
        {
            Id = notificationDto.Id,
            GlobalUserId = notificationDto.GlobalUserId,
            Heading = notificationDto.Heading,
            Message = notificationDto.Message,
            IsRead = notificationDto.IsRead,
            CreatedDate = notificationDto.CreatedDate,
        };
    }
}
