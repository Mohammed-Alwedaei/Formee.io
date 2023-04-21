namespace Subscriptions.BusinessLogic.Dtos.Orders;

public class CouponDto : BaseDto
{
    public string? AdminEmail { get; set; }

    public int Discount { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime ExpireDate { get; set; }

    public bool IsDeleted { get; set; }
}
