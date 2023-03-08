using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analytics.Utilities.Entities;

public class SiteEntity : Entity
{
    [Required]
    [MaxLength(24)]
    public string? ContainerId { get; set; }

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

    public IReadOnlyList<PageHitEntity>? PagesHits { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public CategoryEntity Category { get; set; } = null!;
}
