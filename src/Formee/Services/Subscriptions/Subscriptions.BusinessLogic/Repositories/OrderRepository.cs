using System.Collections.Generic;
using AutoMapper;
using Subscriptions.BusinessLogic.Dtos.Orders;
using Subscriptions.BusinessLogic.Models.Orders;

namespace Subscriptions.BusinessLogic.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public OrderRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderHeaderDto?> GetByIdAsync(int id)
    {
        var orderFromDb = await _context.OrderHeaders
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.Id == id);

        return _mapper.Map<OrderHeaderDto>(orderFromDb ?? new OrderHeaderModel());
    }

    public async Task<List<OrderHeaderDto>> GetAllByUserIdAsync(int userId)
    {
        var orderHeaderFromDb = await _context.OrderHeaders
            .Include(o => o.OrderDetails)
            .Where(o => o.SubscribedUserId == userId)
            .ToListAsync();

        return _mapper.Map<List<OrderHeaderDto>>(orderHeaderFromDb);
    }

    public async Task<OrderHeaderDto?> CreateAsync
        (OrderDetailsDto orderDetails, int userId)
    {
        var orderHeaderDto = new OrderHeaderDto
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

        orderHeaderDto.TotalPrice = subscription.Price;
        orderHeaderDto.TotalPriceAfterDiscount = priceAfterDiscount;

        var orderHeaderModel = _mapper.Map<OrderHeaderModel>(orderHeaderDto);

        _context.OrderHeaders.Add(orderHeaderModel);

        await _context.SaveChangesAsync();

        return orderHeaderDto;
    }
}
