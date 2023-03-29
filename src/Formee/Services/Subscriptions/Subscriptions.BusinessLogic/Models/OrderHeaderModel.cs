using System.ComponentModel.DataAnnotations.Schema;

namespace Subscriptions.BusinessLogic.Models;

public class OrderHeaderModel : BaseModel
{
    public int SubscribedUserId { get; set; }

    [ForeignKey(nameof(SubscribedUserId))]
    public UsersModel SubscribedUser { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalPriceAfterDiscount { get; set; }

    public int OrderDetailsId { get; set; }

    [ForeignKey(nameof(OrderDetailsId))]
    public OrderDetailsModel OrderDetails { get; set; }

    public bool HasConsent { get; set; }

    public static implicit operator OrderHeaderModel
        (OrderHeaderDto orderDto)
    {
        var orderModel = new OrderHeaderModel
        {
            Id = orderDto.Id,
            SubscribedUserId = orderDto.SubscribedUserId,
            SubscribedUser = orderDto.SubscribedUser,
            TotalPrice = orderDto.TotalPrice,
            TotalPriceAfterDiscount = orderDto.TotalPriceAfterDiscount,
            OrderDetailsId = orderDto.OrderDetailsId,
            HasConsent = orderDto.HasConsent,
            CreatedDate = orderDto.CreatedDate
        };

        return orderModel;
    }
}
