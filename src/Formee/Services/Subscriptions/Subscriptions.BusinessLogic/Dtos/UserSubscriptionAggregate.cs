namespace Subscriptions.BusinessLogic.Dtos;

public class UserSubscriptionAggregate
{
    public SubscriptionDto Subscription { get; set; } = new();

    public SubscriptionFeatureDto SubscriptionFeatures { get; set; } = new();

    public OrderHeaderDto OrderHeader { get; set; } = new();
}
