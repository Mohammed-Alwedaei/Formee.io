using Subscriptions.BusinessLogic.Dtos.Orders;

namespace Subscriptions.BusinessLogic.Repositories.IRepository;

public interface ICouponRepository
{
    Task<CouponDto> GetByIdAsync(int id);

    Task<CouponDto> CreateAsync(CouponDto coupon);

    Task<CouponDto> UpdateAsync(CouponDto coupon);

    Task<CouponDto> DeleteAsync(int id);
}
