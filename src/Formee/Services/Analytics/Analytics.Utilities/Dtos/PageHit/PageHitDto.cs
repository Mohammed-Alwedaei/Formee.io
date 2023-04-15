using Analytics.Utilities.Entities;

namespace Analytics.Utilities.Dtos.PageHit;

public class PageHitDto
{
    public int Id { get; set; }

    public int SiteId { get; set; }

    public virtual SiteEntity? Site { get; set; }

    public string? Country { get; set; }

    public string? Route { get; set; }

    public int CategoryId { get; set; }

    public virtual CategoryEntity? Category { get; set; }

    public DateTime CreatedDate { get; set; }
}
