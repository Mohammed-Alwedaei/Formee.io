namespace Client.Web.Utilities.Dtos.Identity;

public class AvatarDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public DateTime UploadedDate { get; set; }
}
