namespace Client.Web.Utilities.Dtos.Subscriptions;

public class SubscriptionDto : BaseDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public decimal AnnualPrice { get; set; }

    public int SubscriptionFeaturesId { get; set; }

    public SubscriptionFeatureDto? SubscriptionFeatures { get; set; }
}
