namespace Domain.Entities;

/// <summary>
/// 
/// </summary>
public class Entity
{
    [Key]
    public int Id { get; set; }

    [JsonIgnore]
    public bool IsDeleted { get; set; } = false;

    [JsonIgnore]
    public bool IsModified { get; set; } = false;

    [JsonIgnore]
    public DateTime? LastModified { get; set; }

    [JsonIgnore]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
