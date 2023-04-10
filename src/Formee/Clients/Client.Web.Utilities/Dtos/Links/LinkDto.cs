namespace Client.Web.Utilities.Dtos.Links;

public class LinkDto
{
    public string ContainerId { get; set; } = null!;

    public int LinkDetailsId { get; set; }

    public LinkDetailsDto LinkDetails { get; set; }

    public string? OriginalUrl { get; set; }

    public string? TargetUrl { get; set; }
}
