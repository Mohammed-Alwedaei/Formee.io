namespace Links.Utilities.Entities;
public class DeleteLinkDto
{
    public int Id { get; set; } 

    public string? ContainerId { get; set; }

    public string? OriginalUrl { get; set; }

    public string? TargetUrl { get; set; }
}
