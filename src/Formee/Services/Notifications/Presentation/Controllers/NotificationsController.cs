﻿using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationsManager _notificationsManager;
    private readonly ILogger<NotificationsController> _logger;

    public NotificationsController(INotificationsManager notificationsManager,
        ILogger<NotificationsController> logger)
    {
        _notificationsManager = notificationsManager;
        _logger = logger;
    }

    [HttpGet("all/{globalUserId:Guid}/{numberOfRecords:int}")]
    public async Task<IActionResult> GetAllNotifications
        (Guid globalUserId, int numberOfRecords)
    {
        _logger.LogInformation("GET: request at /api/subscriptions/features at {datetime}",
            DateTime.Now);

        var result = await _notificationsManager
            .GetAllNotificationsAsync(globalUserId, numberOfRecords);

        if (!result.Any())
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("reads/{notificationId:int}")]
    public async Task<IActionResult> GetAllNotifications
        (int notificationId)
    {
        _logger.LogInformation("GET: request at /api/notifications/reads at {datetime}",
            DateTime.Now);

        var result = await _notificationsManager
            .MarkNotificationAsReadAsync(notificationId);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
