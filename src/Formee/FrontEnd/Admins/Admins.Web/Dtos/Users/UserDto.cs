using System.ComponentModel.DataAnnotations;

namespace Admins.Web.Dtos.Users;

public class UserDto
{
    public Guid Id { get; set; }
    
    public string? Email { get; set; }

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

    public AvatarDto? Avatar { get; set; }

    public int SubscriptionId { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsModified { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public DateTime CreatedDate { get; set; }
}
