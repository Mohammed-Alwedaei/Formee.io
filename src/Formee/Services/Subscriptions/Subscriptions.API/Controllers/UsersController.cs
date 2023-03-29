using Microsoft.AspNetCore.Mvc;
using Subscriptions.BusinessLogic.Dtos;
using Subscriptions.BusinessLogic.Models;
using Subscriptions.BusinessLogic.Repositories.IRepository;

namespace Subscriptions.API.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserRepository _userRepository;

    public UsersController(ILogger<UsersController> logger, 
        IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        _logger.LogInformation("GET: request at /api/users/all at {datetime}", 
            DateTime.Now);

        var result = await _userRepository.GetAllAsync();

        if (!result.Any())
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("all/subscribed/{subscriptionId:int}")]
    public async Task<IActionResult> GetAllSubscribedUsers(int subscriptionId)
    {
        _logger.LogInformation("GET: request at /api/users/all/subscribed/{id} at {datetime}",
            subscriptionId, DateTime.Now);

        var result = await _userRepository
            .GetAllInASubscriptionAsync(subscriptionId);

        if (!result.Any())
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("all/subscribed")]
    public async Task<IActionResult> GetAllSubscribedUsers()
    {
        _logger.LogInformation("GET: request at /api/users/all/subscribed at {datetime}",
            DateTime.Now);

        var result = await _userRepository
            .GetAllSubscribedAsync();

        if (!result.Any())
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserDto user)
    {
        _logger.LogInformation("post: request at /api/users at {datetime}",
            DateTime.Now);

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _userRepository
            .CreateAsync(user);

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        _logger.LogInformation("post: request at /api/users at {datetime}",
            DateTime.Now);

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _userRepository
            .DeleteAsync(userId);

        return Ok(result);
    }
}
