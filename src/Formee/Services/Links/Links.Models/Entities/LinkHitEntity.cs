using System.ComponentModel.DataAnnotations;

namespace Links.Utilities.Entities;

public class LinkHitEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int LinkId { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
