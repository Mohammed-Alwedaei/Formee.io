using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Identity.BusinessLogic.Dtos;

namespace Identity.BusinessLogic.Entities;

public class UserEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string? UserName { get; set; }

    [Required]
    [MaxLength(50)]
    public string? FirstName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string? LastName { get; set; }

    [Required]
    [MaxLength(100)]
    public string? AuthId { get; set; }

    public int? AvatarId { get; set; }

    public AvatarEntity? Avatar { get; set; }

    [Required]
    public int SubscriptionId { get; set; }

    public bool? IsDeleted { get; set; } = false;

    public bool? IsModified { get; set; } = false;

    public DateTime? LastModifiedDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public static implicit operator UserEntity(UserDto user)
    {
        return new UserEntity
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
