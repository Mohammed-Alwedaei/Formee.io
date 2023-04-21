namespace Subscriptions.BusinessLogic.Dtos;

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

    public static implicit operator SubscriptionDto
        (SubscriptionsModel subscription)
    {
        return new SubscriptionDto
        {
            Id = subscription.Id,
            AdminId = subscription.AdminId,
            Name = subscription.Name,
            Description = subscription.Description,
            Price = subscription.Price,
            AnnualPrice = subscription.AnnualPrice,
            IsDefault = subscription.IsDefault,
            IsDeleted = subscription.IsDeleted,
            SubscriptionFeaturesId = subscription.SubscriptionFeaturesId,
            SubscriptionFeatures = subscription.SubscriptionFeatures,
            CreatedDate = subscription.CreatedDate
        };
    }
}
