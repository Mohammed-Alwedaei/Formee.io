using AutoMapper;
using Subscriptions.BusinessLogic.Dtos.Orders;
using Subscriptions.BusinessLogic.Models.Orders;

namespace Subscriptions.BusinessLogic.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CouponRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CouponDto> GetByIdAsync(int id)
    {
        var couponModelFromDb = await _context.Coupons
            .FirstOrDefaultAsync(c => c.Id == id
                                      && c.IsDeleted != true);

        return _mapper.Map<CouponDto>(couponModelFromDb ?? new CouponModel());
    }

    public async Task<List<CouponDto>> GetAllCouponsByFilterAsync(string filter)
    {
        var couponsModelFromDb = filter switch
        {
            "active" => await _context.Coupons
                .Where(c => c.IsDeleted == false)
                .ToListAsync(),

            "deleted" => await _context.Coupons
                .Where(c => c.IsDeleted == true)
                .ToListAsync(),

            _ => await _context.Coupons.ToListAsync()
        };

        return _mapper.Map<List<CouponDto>>(couponsModelFromDb)
               ?? new List<CouponDto>();
    }

    public async Task<CouponDto> CreateAsync(CouponDto couponDto)
    {
        var couponModel  = _mapper.Map<CouponModel>(couponDto);

        var createdCoupon = await _context.Coupons
            .AddAsync(couponModel);

        await _context.SaveChangesAsync();

        return _mapper.Map<CouponDto>(createdCoupon.Entity);
    }

    public async Task<CouponDto> UpdateAsync(CouponDto couponDto)
    {
        var couponModel = _mapper.Map<CouponModel>(couponDto);

        var updatedCoupon = _context.Coupons.
            Update(couponModel);

        await _context.SaveChangesAsync();

        return _mapper.Map<CouponDto>(updatedCoupon.Entity);
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

        return _mapper.Map<CouponDto>(couponToDelete);
    }
}
