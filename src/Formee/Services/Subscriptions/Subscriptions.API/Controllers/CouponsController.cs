using Microsoft.AspNetCore.Mvc;
using Subscriptions.BusinessLogic.Dtos;
using Subscriptions.BusinessLogic.Repositories.IRepository;

namespace Subscriptions.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponsController : ControllerBase
{
    private readonly ICouponRepository _couponRepository;
    private readonly ILogger<CouponsController> _logger;

    public CouponsController(ICouponRepository couponRepository,
        ILogger<CouponsController> logger)
    {
        _couponRepository = couponRepository;
        _logger = logger;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCouponById(int id)
    {
        _logger.LogInformation("GET: request at /api/coupons/{id} at {datetime}",
            id,
            DateTime.Now);

        var result = await _couponRepository.GetByIdAsync(id);

        if (result.Id is 0)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCoupon(CouponDto coupon)
    {
        _logger.LogInformation("POST: request at /api/coupons at {datetime}",
            DateTime.Now);

        if (!ModelState.IsValid) return BadRequest();

        var result = await _couponRepository.CreateAsync(coupon);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCoupon(CouponDto coupon)
    {
        _logger.LogInformation("PUT: request at /api/coupons at {datetime}",
            DateTime.Now);

        if (!ModelState.IsValid) return BadRequest();

        var result = await _couponRepository.UpdateAsync(coupon);

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCoupon(int id)
    {
        _logger.LogInformation("GET: request at /api/coupons/{id} at {datetime}",
            id,
            DateTime.Now);

        var result = await _couponRepository.DeleteAsync(id);

        if (result is not CouponDto)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
