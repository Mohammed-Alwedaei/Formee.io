using System.ComponentModel.DataAnnotations.Schema;

namespace Subscriptions.BusinessLogic.Models;

public class OrderDetailsModel : BaseModel
{
    public int CouponId { get; set; }

    [ForeignKey(nameof(CouponId))]
    public CouponModel? Coupon { get; set; }

    public int SubscriptionId { get; set; }

    [ForeignKey(nameof(SubscriptionId))]
    public SubscriptionsModel Subscription { get; set; }
}
