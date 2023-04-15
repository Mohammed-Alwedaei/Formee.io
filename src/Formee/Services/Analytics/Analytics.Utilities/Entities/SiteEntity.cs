using System.ComponentModel.DataAnnotations;

namespace Analytics.Utilities.Entities;

public class SiteEntity : Entity
{
    [Required]
    [MaxLength(24)]
    public string? ContainerId { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Domain { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(30)]
    public string? Name { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(1024)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Icon { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public virtual CategoryEntity Category { get; set; } = null!;
}
