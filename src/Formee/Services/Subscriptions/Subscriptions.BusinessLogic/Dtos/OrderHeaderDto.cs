namespace Subscriptions.BusinessLogic.Dtos;

public class OrderHeaderDto : BaseDto
{
    public int SubscribedUserId { get; set; }

    public UserDto SubscribedUser { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal TotalPriceAfterDiscount { get; set; }

    public int OrderDetailsId { get; set; }

    public OrderDetailsDto OrderDetails { get; set; }

    public bool HasConsent { get; set; }

    public static implicit operator OrderHeaderDto
        (OrderHeaderModel orderModel)
    {
        var orderDto = new OrderHeaderDto
        {
            Id = orderModel.Id,
            SubscribedUserId = orderModel.SubscribedUserId,
            SubscribedUser = orderModel.SubscribedUser,
            TotalPrice = orderModel.TotalPrice,
            TotalPriceAfterDiscount = orderModel.TotalPriceAfterDiscount,
            OrderDetailsId = orderModel.OrderDetailsId,
            HasConsent = orderModel.HasConsent,
            CreatedDate = orderModel.CreatedDate
        };

        return orderDto;
    }
}
