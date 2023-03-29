namespace Subscriptions.BusinessLogic.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly ApplicationDbContext _context;

    public CouponRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CouponDto> GetByIdAsync(int id)
    {
        return await _context.Coupons
            .FirstOrDefaultAsync(c => c.Id == id 
                                      && c.IsDeleted != true)
               ?? new CouponModel();
    }

    public async Task<CouponDto> CreateAsync(CouponDto coupon)
    {
        var createdCoupon = await _context.Coupons
            .AddAsync(coupon);

        await _context.SaveChangesAsync();

        return createdCoupon.Entity;
    }

    public async Task<CouponDto> UpdateAsync(CouponDto coupon)
    {
        var updatedCoupon = _context.Coupons.Update(coupon);

        await _context.SaveChangesAsync();

        return updatedCoupon.Entity;
    }

    public async Task<CouponDto> DeleteAsync(int id)
    {
        var couponToDelete = await _context.Coupons
            .FirstOrDefaultAsync(c => c.Id == id);

        if (couponToDelete is null)
        {
            return new CouponDto();
        }

        couponToDelete.IsDeleted = true;

        _context.Coupons.Update(couponToDelete);

        await _context.SaveChangesAsync();

        return couponToDelete;
    }
}
