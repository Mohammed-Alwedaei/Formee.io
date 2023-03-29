namespace Domain.Entities;

public class VisitorEntity : Entity
{
    public string? FullName { get; set; }

    public string? Email { get; set; }

    public int SessionId { get; set; }

    public List<SessionEntity> Sessions { get; set; }
}
