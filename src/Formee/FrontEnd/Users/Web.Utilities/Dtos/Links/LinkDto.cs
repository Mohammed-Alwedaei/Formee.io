using Newtonsoft.Json;

namespace Client.Web.Utilities.Dtos.Links;

public class LinkDto
{
    public int Id { get; set; }

    public string ContainerId { get; set; } = null!;

    public Guid UserId { get; set; }

    [JsonIgnore]
    public string? Name { get; set; }

    public int LinkDetailsId { get; set; }

    public LinkDetailsDto? LinkDetails { get; set; }

    public string? OriginalUrl { get; set; }

    public string? TargetUrl { get; set; }
}
