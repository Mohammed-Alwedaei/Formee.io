using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Web.Utilities.Dtos.Analytics;

public class PageHitDto : BaseDto
{
    [Required]
    public int SiteId { get; set; }

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
    public CategoryDto Category { get; set; } = null!;
}