namespace Domain.Entities;

/// <summary>
/// 
/// </summary>
public class Entity
{
    [Key]
    public int Id { get; set; }

    public bool? IsDeleted { get; set; } = false;

    public bool? IsModified { get; set; } = false;

    public DateTime? LastModified { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
