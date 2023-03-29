namespace Domain.Entities;

public class AgentEntity : Entity
{
    public string ContainerId { get; set; } = null!;

    public Guid UserId { get; set; }
}
