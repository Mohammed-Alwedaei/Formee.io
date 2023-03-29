using System.ComponentModel.DataAnnotations;

namespace Client.Web.Utilities.Dtos.Forms;

public class FormDto
{
    public int Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public int DetailsId { get; set; }

    public DetailsDto? Details { get; set; }

    public virtual List<FieldDto>? Fields { get; set; }

    public DateTime? LastModified { get; set; }

    public DateTime CreatedDate { get; set; }
}
