namespace Domain.Entities;

public class FieldsWarehouseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int FieldId { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(1024)]
    public string Value { get; set; } = null!;

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
