using Subscriptions.BusinessLogic.Dtos;

namespace Subscriptions.BusinessLogic.Models;

public class CouponModel : BaseModel
{
    public int CreatedBy { get; set; }

    public int Discount { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime ExpireDate { get; set; }

    public bool IsDeleted { get; set; }

    public static implicit operator CouponModel(CouponDto coupon)
    {
        var couponDto = new CouponModel
        {
            Id = coupon.Id,
            CreatedBy = coupon.CreatedBy,
            Discount = coupon.Discount,
            Name = coupon.Name,
            Description = coupon.Description,
            ExpireDate = coupon.ExpireDate,
            IsDeleted = coupon.IsDeleted,
            CreatedDate = coupon.CreatedDate,
        };

        return couponDto;
    }
}
