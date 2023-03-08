using System.ComponentModel.DataAnnotations;

namespace Analytics.Utilities.Entities;
public class BaseEntity
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
