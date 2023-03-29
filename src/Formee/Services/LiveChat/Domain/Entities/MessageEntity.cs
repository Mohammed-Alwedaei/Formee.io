namespace Domain.Entities;

public class MessageEntity : Entity
{
    public int SessionId { get; set; }  

    public int SenderId { get; set; }

    [Required]
    [MaxLength(1024)]
    public string? Message { get; set; }
}
