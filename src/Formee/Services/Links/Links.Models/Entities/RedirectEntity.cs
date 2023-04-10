namespace Links.Utilities.Entities;

public class RedirectEntity
{
    public int Id { get; set; }

    public string? OriginalUrl { get; set; }

    public string? TargetUrl { get; set; }
}
