using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.BusinessLogic.Dtos.Users;
using Subscriptions.BusinessLogic.Repositories.IRepository;

namespace Subscriptions.API.Controllers;

[ApiController]
[Authorize]
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

    [AllowAnonymous]
    [HttpGet("subscription/{userId:Guid}")]
    public async Task<IActionResult> GetUserSubscriptions(Guid userId)
    {
        _logger.LogInformation("GET: request at /api/users/all/subscription/{userId} at {datetime}",
            userId,
            DateTime.Now);

        var result = await _userRepository.GetSubscriptionByIdAsync(userId);

        if (result.Id is 0)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [AllowAnonymous]
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

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        _logger.LogInformation("post: request at /api/users at {datetime}",
            DateTime.Now);

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _userRepository
            .DeleteAsync(id);

        return Ok(result);
    }
}
