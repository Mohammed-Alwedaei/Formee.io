

namespace Subscriptions.BusinessLogic.Dtos;

public class CouponDto : BaseDto
{
    public int AdminId { get; set; }

    public int Discount { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime ExpireDate { get; set; }

    public bool IsDeleted { get; set; }


    public static implicit operator CouponDto(CouponModel coupon)
    {
        var couponDto = new CouponDto
        {
            Id = coupon.Id,
            AdminId = coupon.AdminId,
            Name = coupon.Name,
            Discount = coupon.Discount,
            Description = coupon.Description,
            ExpireDate = coupon.ExpireDate,
            IsDeleted = coupon.IsDeleted,
            CreatedDate = coupon.CreatedDate,
        };

        return couponDto;
    }
}
