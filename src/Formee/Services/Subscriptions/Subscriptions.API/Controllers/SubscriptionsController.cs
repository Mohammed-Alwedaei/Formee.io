﻿using Microsoft.AspNetCore.Mvc;
using Subscriptions.BusinessLogic.Dtos;
using Subscriptions.BusinessLogic.Repositories;
using Subscriptions.BusinessLogic.Repositories.IRepository;

namespace Subscriptions.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly ILogger<SubscriptionsController> _logger;

    public SubscriptionsController
        (ISubscriptionRepository subscriptionRepository,
            ILogger<SubscriptionsController> logger)
    {
        _subscriptionRepository = subscriptionRepository;
        _logger = logger;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSubscriptionById(int id)
    {
        _logger.LogInformation("GET: request at /api/subscriptions/{id} at {datetime}",
        id,
        DateTime.Now);

        var result = await _subscriptionRepository.GetOneById(id);

        if (result.Id is 0)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllSubscriptions()
    {
        _logger.LogInformation("GET: request at /api/subscriptions/all at {datetime}",

            DateTime.Now);

        var result = await _subscriptionRepository.GetAllAsync();

        if (!result.Any())
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("all/{adminId:int}")]
    public async Task<IActionResult> GetAllSubscriptionByAdminId(int adminId)
    {
        _logger.LogInformation("GET: request at /api/subscriptions/all at {datetime}",

            DateTime.Now);

        var result = await _subscriptionRepository
            .GetAllByAdminIdAsync(adminId);

        if (!result.Any())
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription
        (SubscriptionDto subscription)
    {
        _logger.LogInformation("POST: request at /api/subscriptions at {datetime}",
            DateTime.Now);

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _subscriptionRepository
            .CreateAsync(subscription);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSubscription
        (SubscriptionDto subscription)
    {
        _logger.LogInformation("PUT: request at /api/subscriptions at {datetime}",
            DateTime.Now);

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _subscriptionRepository
            .UpdateAsync(subscription);

        return Ok(result);
    }

    [HttpPut("users/{userId:int}/{subscriptionId:int}")]
    public async Task<IActionResult> AddSubscriptionToUser
        (int userId, int subscriptionId)
    {
        _logger.LogInformation("PUT: request at /api/subscriptions/users/{userId}/{subscriptionId} at {datetime}",
            userId,
            subscriptionId,
            DateTime.Now);

        var result = await _subscriptionRepository
            .AddSubscriptionToUserAsync(userId, subscriptionId);

        if (result.Id is 0)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("users/{userId:int}")]
    public async Task<IActionResult> RemoveSubscriptionFromUser
        (int userId)
    {
        _logger.LogInformation("DELETE: request at /api/subscriptions/users/{userId} at {datetime}",
            userId,
            DateTime.Now);

        var result = await _subscriptionRepository
            .RemoveSubscriptionFromUserAsync(userId);

        if (result.Id is 0)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSubscription
        (int id)
    {
        _logger.LogInformation("DELETE: request at /api/subscriptions/{id} at {datetime}",
            id,
            DateTime.Now);

        var result = await _subscriptionRepository.DeleteAsync(id);

        if (result.Id is 0)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
