namespace Analytics.Utilities.Dtos.PageHit;

public class CreatePageHitDto
{
    public int SiteId { get; set; }

    public string DeviceId { get; set; }

    public string? Country { get; set; }

    public string? Route { get; set; }

    public int CategoryId { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
