using System.ComponentModel.DataAnnotations;

namespace Client.Web.Utilities.Dtos.Identity;

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

    public AvatarDto Avatar { get; set; }

    public int SubscriptionId { get; set; }

    public bool? IsDeleted { get; set; } = false;

    public bool? IsModified { get; set; } = false;

    public DateTime? LastModifiedDate { get; set; }

    public DateTime CreatedDate { get; set; }
}
