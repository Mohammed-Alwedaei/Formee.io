namespace Domain.Entities;

public class SessionEntity : Entity
{
    public string ContainerId { get; set; } = null!;

    public int AgentId { get; set; }

    public AgentEntity Agent { get; set; } = null!;

    public int VisitorId { get; set; }

    [ForeignKey(name: nameof(VisitorId))]
    public VisitorEntity? Visitor { get; set; }

    public List<MessageEntity>? Messages { get; set; }
}
