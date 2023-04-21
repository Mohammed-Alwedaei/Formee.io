using System.ComponentModel.DataAnnotations;

namespace Client.Web.Utilities.Dtos.Analytics;

public class SiteDto
{
    public int Id { get; set; }

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

    [Required]
    public int CategoryId { get; set; }
}
