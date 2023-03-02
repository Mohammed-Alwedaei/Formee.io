namespace Domain.Entities;

/// <summary>
/// FieldEntity contains the form elements
/// Relationship type: many-to-one
/// Relationship with: FormEntity
/// </summary>
public class FieldEntity
{
    [Key]
    public int Id { get; set; }

    public int FormId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(FormId))]
    public FormEntity? Form { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(15)]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(2)]
    [MaxLength(15)]
    public string DataType { get; set; } = null!;
}
