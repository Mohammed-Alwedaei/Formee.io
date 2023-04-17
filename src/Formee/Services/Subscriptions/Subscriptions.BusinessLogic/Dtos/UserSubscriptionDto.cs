namespace Subscriptions.BusinessLogic.Dtos;

public class UserSubscriptionDto
{
    public int Id { get; set; }

    public int UserId { get; set; }
    
    public UserDto User { get; set; }

    public int SubscriptionId { get; set; }
    
    public SubscriptionDto Subscription { get; set; }

    public DateTime CreatedDate { get; set; }
    
    public static implicit operator UserSubscriptionDto(UserSubscriptionModel userSubscription)
    {
        return new UserSubscriptionDto()
        {
            Id = userSubscription.Id,
            UserId = userSubscription.UserId,
            User = userSubscription.User,
            SubscriptionId = userSubscription.SubscriptionId,
            Subscription = userSubscription.Subscription,
            CreatedDate = userSubscription.CreatedDate
        };
    }
}