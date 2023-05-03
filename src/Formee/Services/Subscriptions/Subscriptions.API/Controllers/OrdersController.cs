using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.BusinessLogic.Dtos.Orders;
using Subscriptions.BusinessLogic.Repositories.IRepository;

namespace Subscriptions.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<UsersController> _logger;

    public OrdersController(IOrderRepository orderRepository,
        ILogger<UsersController> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        _logger.LogInformation("GET: request at /api/orders/{id} at {datetime}",
            id,
        DateTime.Now);

        var result = await _orderRepository.GetByIdAsync(id);

        if (result is not { Id: > 0 })
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("all/{userId:int}")]
    public async Task<IActionResult> GetAllOrdersByUserId(int userId)
    {
        _logger.LogInformation("GET: request at /api/orders/all/{userId} at {datetime}",
            userId,
            DateTime.Now);

        var result = await _orderRepository.GetAllByUserIdAsync(userId);

        if (!result.Any())
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("{userId:int}")]
    public async Task<IActionResult> GetAllOrdersByUserId
        (OrderDetailsDto orderDetails, int userId)
    {
        _logger.LogInformation("POST: request at /api/orders/{userId} at {datetime}",
            userId,
            DateTime.Now);

        var result = await _orderRepository
            .CreateAsync(orderDetails, userId);

        return Ok(result);
    }
}
