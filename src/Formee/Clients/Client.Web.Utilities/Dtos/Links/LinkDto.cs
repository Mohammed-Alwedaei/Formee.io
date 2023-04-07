namespace Client.Web.Utilities.Dtos.Links;

public class LinkDto
{
    public int Id { get; set; }

    public string ContainerId { get; set; } = null!;

    public string? OriginalUrl { get; set; }

    public string? TargetUrl { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
