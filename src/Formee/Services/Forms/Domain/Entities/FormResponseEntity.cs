namespace Domain.Entities;

public class FormResponseEntity : Entity
{
    public int FormId { get; set; }

    public virtual List<FormResponseFieldsEntity> FormResponseFields 
    { get; set; } = null!;
}
