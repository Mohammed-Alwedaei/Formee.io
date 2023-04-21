using System.ComponentModel.DataAnnotations;

namespace Client.Web.Utilities.Dtos.Forms;

public class DetailsDto
{
    public int Id { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(30)]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(10)]
    [MaxLength(1024)]
    public string Description { get; set; } = null!;
}
