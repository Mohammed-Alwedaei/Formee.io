namespace Domain.Entities;

public class Entity
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}