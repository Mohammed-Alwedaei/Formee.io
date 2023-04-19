using Identity.BusinessLogic.Entities;
using System.ComponentModel.DataAnnotations;

namespace Identity.BusinessLogic.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    
    [Required]
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; } = null!;

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

    public AvatarDto? Avatar { get; set; } = new();

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
}
