namespace Client.Web.Utilities.Dtos.Forms;

public class FormResponseDto : Dto
{
    public int FormId { get; set; }

    public virtual List<FormResponseFieldsDto> FormResponseFields
    { get; set; } = null!;
}
