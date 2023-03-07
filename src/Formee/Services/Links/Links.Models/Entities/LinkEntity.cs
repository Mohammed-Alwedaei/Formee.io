namespace Links.Utilities.Entities;

public class LinkEntity : Entity
{
    public string ContainerId { get; set; } = null!;

    public string? OriginalUrl { get; set; }
    
    public string? TargetUrl { get; set; }
}