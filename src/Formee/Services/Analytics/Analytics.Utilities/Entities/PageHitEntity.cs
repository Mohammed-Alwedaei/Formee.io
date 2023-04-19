using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analytics.Utilities.Entities;

public class PageHitEntity : BaseEntity
{
    [Required]
    public int SiteId { get; set; }

    public virtual SiteEntity? Site { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string? Country { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(250)]
    public string? Route { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public virtual CategoryEntity Category { get; set; }
}