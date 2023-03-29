using System.ComponentModel.DataAnnotations;

namespace Client.Web.Utilities.Dtos.Forms;

public class FieldDto
{
    public int Id { get; set; }

    public int FormId { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(15)]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(2)]
    [MaxLength(15)]
    public string DataType { get; set; } = null!;

    public DateTime? LastModified { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
