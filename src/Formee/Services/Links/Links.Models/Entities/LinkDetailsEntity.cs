using System.ComponentModel.DataAnnotations;

namespace Links.Utilities.Entities;

public class LinkDetailsEntity
{
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string Name { get; set; } = null!;

    public string Domain { get; set; }
}
