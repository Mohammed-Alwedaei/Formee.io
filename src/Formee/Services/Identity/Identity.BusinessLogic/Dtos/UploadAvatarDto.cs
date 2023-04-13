namespace Identity.BusinessLogic.Dtos;

public class UploadAvatarDto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public string FileName { get; set; } = null!;

    public DateTime PublishDate { get; set; } = DateTime.Now;
}
