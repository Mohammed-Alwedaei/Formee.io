using System.Collections.Generic;

namespace Subscriptions.BusinessLogic.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderHeaderDto?> GetByIdAsync(int id)
    {
        var orderFromDb = await _context.OrderHeaders
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.Id == id);

        if(orderFromDb is null) return new OrderHeaderDto();

        return orderFromDb;
    }

    public async Task<List<OrderHeaderDto>> GetAllByUserIdAsync(int userId)
    {
        return await _context.OrderHeaders
                   .Include(o => o.OrderDetails)
                   .Where(o => o.SubscribedUserId == userId)
                   .Select(o => (OrderHeaderDto)o)
                   .ToListAsync()
               ?? new List<OrderHeaderDto>();
    }

    public async Task<OrderHeaderDto?> CreateAsync
        (OrderDetailsDto orderDetails, int userId)
    {
        var orderHeader = new OrderHeaderDto
        {
            SubscribedUserId = userId,
            OrderDetails = orderDetails,
            HasConsent = true
        };

        var isValidCoupon = await _context.Coupons
            .FirstOrDefaultAsync(c => c.Id == orderDetails.CouponId
                                      && c.IsDeleted != true);

        var subscription = await _context.Subscriptions
            .FirstOrDefaultAsync(s => s.Id == orderDetails.SubscriptionId
                                      && s.IsDeleted != true);

        //calculate the total price
        decimal priceAfterDiscount;

        // calculate the discount 
        if (isValidCoupon is not null)
        {
            priceAfterDiscount = orderDetails.IsMonthly is 1
                ? (subscription.Price * isValidCoupon.Discount) / 100
                : (subscription.AnnualPrice * isValidCoupon.Discount) / 100;
        }
        else
        {
            priceAfterDiscount = orderDetails.IsMonthly is 1
                ? subscription.Price
                : subscription.AnnualPrice;
        }

        orderHeader.TotalPrice = subscription.Price;
        orderHeader.TotalPriceAfterDiscount = priceAfterDiscount;

        _context.OrderHeaders.Add(orderHeader);

        await _context.SaveChangesAsync();

        return orderHeader;
    }
}
