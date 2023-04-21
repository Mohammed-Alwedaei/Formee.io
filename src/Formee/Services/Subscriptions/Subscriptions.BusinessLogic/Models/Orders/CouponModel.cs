namespace Subscriptions.BusinessLogic.Models.Orders;

public class CouponModel : BaseModel
{
    public string? AdminEmail { get; set; }

    public int Discount { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime ExpireDate { get; set; }

    public bool IsDeleted { get; set; }
}
