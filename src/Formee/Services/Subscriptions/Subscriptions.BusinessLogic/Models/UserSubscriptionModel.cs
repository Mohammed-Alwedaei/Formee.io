using System.ComponentModel.DataAnnotations;

namespace Subscriptions.BusinessLogic.Models;

public class UserSubscriptionModel
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UsersModel User { get; set; }

    public int SubscriptionId { get; set; }

    [ForeignKey(nameof(SubscriptionId))]
    public SubscriptionsModel SubscriptionsModel { get; set; }

    public DateTime CreatedDate { get; set; }
}
