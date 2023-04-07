using System.ComponentModel.DataAnnotations;

namespace Links.Utilities.Entities;

public class LinkEntity : Entity
{
    [Required]
    [StringLength(24)]
    public string ContainerId { get; set; } = null!;

    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(1024)]
    public string? OriginalUrl { get; set; }
    
    public string? TargetUrl { get; set; }
}