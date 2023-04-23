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
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; } = null!;

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
    
    [Required]
    [MaxLength(50)]
    public string? Job { get; set; }
    
    [Required]
    [MaxLength(2048)]
    public string? Bio { get; set; }
    
    [Required]
    [Phone]
    [MaxLength(40)]
    public string? PhoneNumber { get; set; }
    
    [Required]
    public DateTime? BirthDate { get; set; }

    public int? AvatarId { get; set; }

    [ForeignKey(nameof(AvatarId))]
    public AvatarEntity? Avatar { get; set; } = new();

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
            Email = user.Email,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            AuthId = user.AuthId,
            Job = user.Job,
            Bio = user.Bio,
            PhoneNumber = user.PhoneNumber,
            BirthDate = user.BirthDate,
            AvatarId = user.AvatarId,
            Avatar = user.Avatar,
            SubscriptionId = user.SubscriptionId,
            IsDeleted = user.IsDeleted,
            IsModified = user.IsModified,
            LastModifiedDate = user.LastModifiedDate,
            CreatedDate = user.CreatedDate,
        };
    }

    public static implicit operator UserEntity(UpsertUserDto user)
    {
        return new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = user.Email,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            AuthId = user.AuthId,
            Job = user.Job,
            Bio = user.Bio,
            PhoneNumber = user.PhoneNumber,
            BirthDate = user.BirthDate,
            SubscriptionId = 0,
            IsDeleted = false,
            IsModified = false,
            LastModifiedDate = null,
            CreatedDate = DateTime.UtcNow,
        };
    }
}
