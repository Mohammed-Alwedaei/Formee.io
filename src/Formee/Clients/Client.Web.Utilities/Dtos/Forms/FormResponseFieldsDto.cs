using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Web.Utilities.Dtos.Forms;

public class FormResponseFieldsDto
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int FieldId { get; set; }

    [Required]
    public int FormResponseId { get; set; }


    [ForeignKey(nameof(FormResponseId))]
    public FormResponseDto? FormResponse { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(1024)]
    public string Value { get; set; } = null!;


    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
