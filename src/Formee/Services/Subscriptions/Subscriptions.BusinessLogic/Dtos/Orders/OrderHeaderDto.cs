using Subscriptions.BusinessLogic.Dtos.Users;

namespace Subscriptions.BusinessLogic.Dtos.Orders;

public class OrderHeaderDto : BaseDto
{
    public int SubscribedUserId { get; set; }

    public UserDto SubscribedUser { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal TotalPriceAfterDiscount { get; set; }

    public int OrderDetailsId { get; set; }

    public OrderDetailsDto OrderDetails { get; set; }

    public bool HasConsent { get; set; }
}
