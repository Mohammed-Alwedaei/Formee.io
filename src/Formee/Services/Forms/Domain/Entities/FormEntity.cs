namespace Domain.Entities;

/// <summary>
/// FormEntity contains the Foreign keys
/// Relationship type: [one-to-many, one-to-one]
/// Relationship with: [FieldEntity, DetailsEntity]
/// </summary>
public class FormEntity : Entity
{
    [Required]
    public int SiteId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public int DetailsId { get; set; }

    [ForeignKey(nameof(DetailsId))]
    public DetailsEntity? Details { get; set; }

    public virtual List<FieldEntity>? Fields { get; set; }
}