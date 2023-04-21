using System.ComponentModel.DataAnnotations;
using Subscriptions.BusinessLogic.Models.Subscriptions;

namespace Subscriptions.BusinessLogic.Models.Orders;

public class OrderDetailsModel : BaseModel
{
    [Required]
    public int CouponId { get; set; }

    [ForeignKey(nameof(CouponId))]
    public CouponModel? Coupon { get; set; }

    [Required]
    public int SubscriptionId { get; set; }

    [ForeignKey(nameof(SubscriptionId))]
    public SubscriptionModel Subscription { get; set; }
}
