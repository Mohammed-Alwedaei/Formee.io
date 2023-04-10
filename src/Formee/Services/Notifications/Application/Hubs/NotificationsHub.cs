using Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.Hubs;

public class NotificationsHub : Hub
{
    private readonly INotificationsManager _notificationsManager;
    private readonly ILogger<NotificationsHub> _logger;

    public NotificationsHub(INotificationsManager notificationsManager, 
        ILogger<NotificationsHub> logger)
    {
        _notificationsManager = notificationsManager;
        _logger = logger;
    }

    public async Task MarkNotificationAsReadRequest(int id)
    {
        _logger.LogInformation("Mark as read request in notifications api for entity of id: {id}", id);
        await _notificationsManager.MarkNotificationAsReadAsync(id);
    }
}