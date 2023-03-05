namespace Domain.Entities;

public class FormResponseFieldsEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int FieldId { get; set; }

    [Required]
    public int FormResponseId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(FormResponseId))]
    public FormResponseEntity? FormResponse { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(1024)]
    public string Value { get; set; } = null!;

    [JsonIgnore]
    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
