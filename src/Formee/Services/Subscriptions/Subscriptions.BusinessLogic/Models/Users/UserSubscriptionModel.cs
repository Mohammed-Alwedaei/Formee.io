using Subscriptions.BusinessLogic.Models.Subscriptions;

namespace Subscriptions.BusinessLogic.Models.Users;

public class UserSubscriptionModel : BaseModel
{
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserModel User { get; set; }

    public int SubscriptionId { get; set; }

    [ForeignKey(nameof(SubscriptionId))]
    public SubscriptionModel Subscription { get; set; }
}