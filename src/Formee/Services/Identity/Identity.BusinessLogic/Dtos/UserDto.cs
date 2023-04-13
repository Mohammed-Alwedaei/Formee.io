using Identity.BusinessLogic.Entities;
using System.ComponentModel.DataAnnotations;

namespace Identity.BusinessLogic.Dtos;

public class UserDto
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string AuthId { get; set; } = null!;

    public int? AvatarId { get; set; }

    public AvatarDto? Avatar { get; set; }

    public int SubscriptionId { get; set; }

    public bool? IsDeleted { get; set; } = false;

    public bool? IsModified { get; set; } = false;

    public DateTime? LastModifiedDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public static implicit operator UserDto(UserEntity user)
    {
        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            AuthId = user.AuthId,
            AvatarId = user.AvatarId,
            Avatar = user.Avatar,
            SubscriptionId = user.SubscriptionId,
            IsDeleted = user.IsDeleted,
            IsModified = user.IsModified,
            CreatedDate = user.CreatedDate,
        };
    }
}
