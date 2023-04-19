namespace Subscriptions.BusinessLogic.Models;

public class SubscriptionsModel : BaseModel
{
    public int AdminId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public decimal AnnualPrice { get; set; }

    public bool IsDefault { get; set; }

    public bool IsDeleted { get; set; }

    public int SubscriptionFeaturesId { get; set; }

    [ForeignKey(nameof(SubscriptionFeaturesId))]
    public SubscriptionFeaturesModel SubscriptionFeatures { get; set; }

    public static implicit operator SubscriptionsModel
        (SubscriptionDto subscriptionDto)
    {
        var subscriptionModel = new SubscriptionsModel
        {
            Id = subscriptionDto.Id,
            AdminId = subscriptionDto.AdminId,
            Name = subscriptionDto.Name,
            Description = subscriptionDto.Description,
            Price = subscriptionDto.Price,
            AnnualPrice = subscriptionDto.AnnualPrice,
            IsDeleted = subscriptionDto.IsDeleted,
            IsDefault = subscriptionDto.IsDefault,
            SubscriptionFeaturesId = subscriptionDto.SubscriptionFeaturesId,
            SubscriptionFeatures = subscriptionDto.SubscriptionFeatures,
            CreatedDate = subscriptionDto.CreatedDate
        };

        return subscriptionModel;
    }
}
