using System.ComponentModel.DataAnnotations;

namespace Links.Utilities.Entities;

public class LinkEntity : Entity
{
    [Required]
    [StringLength(24)]
    public string ContainerId { get; set; } = null!;

    public int LinkDetailsId { get; set; }

    public LinkDetailsEntity LinkDetails { get; set; }

    [Required]
    [MaxLength(1024)]
    public string? OriginalUrl { get; set; }
    
    public string? TargetUrl { get; set; }
}