namespace Subscriptions.BusinessLogic.Dtos.Orders;

public class OrderDetailsDto : BaseDto
{
    public int IsMonthly { get; set; }

    public int CouponId { get; set; }

    public int SubscriptionId { get; set; }
}
