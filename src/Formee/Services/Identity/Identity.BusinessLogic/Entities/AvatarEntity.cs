using System.ComponentModel.DataAnnotations;

namespace Identity.BusinessLogic.Entities;

public class AvatarEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public Guid UserId { get; set; }

    public UserEntity User { get; set; }

    public bool? IsDeleted { get; set; } = false;

    public DateTime? DeletedDate { get; set; }

    public DateTime UploadedDate { get; set; } = DateTime.Now;
}
