using Microsoft.AspNetCore.Http;

namespace Identity.BusinessLogic.Models;

public class UploadAvatarModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public string FileName { get; set; } = null!;

    public DateTime PublishDate { get; set; } = DateTime.Now;
}
