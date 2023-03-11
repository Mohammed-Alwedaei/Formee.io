using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.BusinessLogic.Entities;

public class UserEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string AuthId { get; set; } = null!;

    public int? AvatarId { get; set; }

    [ForeignKey(nameof(AvatarId))]
    public AvatarEntity Avatar { get; set; }

    [Required]
    public int SubscriptionId { get; set; }

    public bool? IsDeleted { get; set; } = false;

    public bool? IsModified { get; set; } = false;

    public DateTime? LastModifiedDate { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
