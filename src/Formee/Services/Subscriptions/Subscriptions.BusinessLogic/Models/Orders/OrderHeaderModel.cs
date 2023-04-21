using Subscriptions.BusinessLogic.Models.Users;

namespace Subscriptions.BusinessLogic.Models.Orders;

public class OrderHeaderModel : BaseModel
{
    public int SubscribedUserId { get; set; }

    [ForeignKey(nameof(SubscribedUserId))]
    public UserModel SubscribedUser { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalPriceAfterDiscount { get; set; }

    public int OrderDetailsId { get; set; }

    [ForeignKey(nameof(OrderDetailsId))]
    public OrderDetailsModel OrderDetails { get; set; }

    public bool HasConsent { get; set; }
}
