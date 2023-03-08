using System.ComponentModel.DataAnnotations;

namespace Analytics.Utilities.Entities;
public class CategoryEntity : Entity
{
    [Required]
    [MinLength(5)]
    [MaxLength(50)]
    public string? Name { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(1024)]
    public string? Description { get; set; }

    [Required]
    public Guid UserId { get; set; }
}
