namespace Admins.Web.Dtos.Subscriptions;

public class SubscriptionDto : BaseDto
{
    public int AdminId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public decimal AnnualPrice { get; set; }

    public bool IsDefault { get; set; }

    public int SubscriptionFeaturesId { get; set; }

    public SubscriptionFeatureDto SubscriptionFeatures { get; set; }

    public bool IsDeleted { get; set; }
}
