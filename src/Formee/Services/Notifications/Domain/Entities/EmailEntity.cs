namespace Domain.Entities;

public class EmailEntity : Entity
{
    [Required]
    [MinLength(10)]
    [MaxLength(255)]
    public string? Email { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(100)]
    public string? Title { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(255)]
    public string? Body { get; set; }

    public static implicit operator EmailEntity(EmailDto emailDto)
    {
        return new EmailEntity
        {
            Id = emailDto.Id,
            Email = emailDto.Email,
            Title = emailDto.Title,
            Body = emailDto.Body,
            CreatedDate = emailDto.CreatedDate,
        };
    }
}
