using System.ComponentModel.DataAnnotations;

namespace Links.Utilities.Entities;

public class Entity
{
    [Key]
    public int Id { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime? DeletedDate { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
