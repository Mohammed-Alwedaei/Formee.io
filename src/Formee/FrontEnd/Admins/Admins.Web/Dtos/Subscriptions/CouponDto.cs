namespace Admins.Web.Dtos.Subscriptions;

public class CouponDto : BaseDto
{
    public int CreatedBy { get; set; }

    public int Discount { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime ExpireDate { get; set; }

    public bool IsDeleted { get; set; }
}
