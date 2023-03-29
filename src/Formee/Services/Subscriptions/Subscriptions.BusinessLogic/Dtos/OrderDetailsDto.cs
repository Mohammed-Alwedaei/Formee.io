namespace Subscriptions.BusinessLogic.Dtos;

public class OrderDetailsDto : BaseDto
{
    public int IsMonthly { get; set; }

    public int CouponId { get; set; }

    [ForeignKey(nameof(CouponId))]
    public CouponDto? Coupon { get; set; }

    public int SubscriptionId { get; set; }

    [ForeignKey(nameof(SubscriptionId))]
    public SubscriptionDto Subscription { get; set; }
}
