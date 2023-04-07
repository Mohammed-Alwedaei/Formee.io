namespace Domain.Dtos;

public class EmailDto : Dto
{
    public string? Email { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }


    public static implicit operator EmailDto(EmailEntity emailEntity)
    {
        return new EmailDto
        {
            Id = emailEntity.Id,
            Email = emailEntity.Email,
            Title = emailEntity.Title,
            Body = emailEntity.Body,
            CreatedDate = emailEntity.CreatedDate,
        };
    }
}
