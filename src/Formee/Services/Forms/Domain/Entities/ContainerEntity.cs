namespace Domain.Entities;
public class ContainerEntity : Entity
{
    public string BsonId { get; set; } = null!;

    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;
}
