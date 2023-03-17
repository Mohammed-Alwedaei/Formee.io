using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Client.Web.Utilities.Dtos;

public class UserDto
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string AuthId { get; set; } = null!;

    public int? AvatarId { get; set; }

    public int SubscriptionId { get; set; }

    public bool? IsDeleted { get; set; } = false;

    public bool? IsModified { get; set; } = false;

    public DateTime? LastModifiedDate { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
