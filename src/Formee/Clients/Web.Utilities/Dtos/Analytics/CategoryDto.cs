using System.ComponentModel.DataAnnotations;
using Client.Web.Utilities.Dtos.Forms;

namespace Client.Web.Utilities.Dtos.Analytics;
public class CategoryDto : Dto
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
