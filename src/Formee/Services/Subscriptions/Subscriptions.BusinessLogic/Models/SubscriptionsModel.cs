namespace Subscriptions.BusinessLogic.Models;

public class SubscriptionsModel : BaseModel
{
    public int AdminId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public decimal AnnualPrice { get; set; }

    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public List<UsersModel> User { get; set; }

    public bool IsDeleted { get; set; }

    public static implicit operator SubscriptionsModel
        (SubscriptionDto subscriptionDto)
    {
        var subscriptionModel = new SubscriptionsModel
        {
            Name = subscriptionDto.Name,
            Description = subscriptionDto.Description,
            Price = subscriptionDto.Price,
            AnnualPrice = subscriptionDto.AnnualPrice,
            UserId = subscriptionDto.UserId
        };

        return subscriptionModel;
    }
}
