namespace Admins.Web.Dtos.Users;

public class AvatarDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public Guid UserId { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public DateTime UploadedDate { get; set; }
}
