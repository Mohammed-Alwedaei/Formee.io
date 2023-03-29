namespace Subscriptions.BusinessLogic.Dtos;

public class SubscriptionDto : BaseDto
{
    public int AdminId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public decimal AnnualPrice { get; set; }

    public int UserId { get; set; }

    public bool IsDeleted { get; set; }

    public static implicit operator SubscriptionDto
        (SubscriptionsModel subscription)
    {
        var subscriptionDto = new SubscriptionDto
        {
            AdminId = subscription.AdminId,
            Name = subscription.Name, 
            Description = subscription.Description,
            Price = subscription.Price,
            AnnualPrice = subscription.AnnualPrice,
            UserId = subscription.UserId,
            IsDeleted = subscription.IsDeleted
        };

        return subscriptionDto;
    }
}
