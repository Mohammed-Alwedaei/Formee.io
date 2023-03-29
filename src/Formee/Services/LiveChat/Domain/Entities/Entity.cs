namespace Domain.Entities;

public class Entity
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime DeletedDate { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}