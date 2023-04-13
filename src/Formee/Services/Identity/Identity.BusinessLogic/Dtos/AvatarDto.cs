using Identity.BusinessLogic.Entities;

namespace Identity.BusinessLogic.Dtos;

public class AvatarDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid UserId { get; set; }

    public bool? IsDeleted { get; set; } = false;

    public DateTime? DeletedDate { get; set; }

    public DateTime UploadedDate { get; set; }

    public static implicit operator AvatarDto(AvatarEntity avatar)
    {
        return new AvatarDto
        {
            Id = avatar.Id,
            Name = avatar.Name,
            UserId = avatar.UserId,
            IsDeleted = avatar.IsDeleted,
            DeletedDate = avatar.DeletedDate,
            UploadedDate = avatar.UploadedDate,
        };
    }
}
