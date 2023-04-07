using Domain.Dtos;
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

    public async Task HandleMarkAsReadNotification(int notificationId)
    {
        var notificationToUpdate = await _notificationsManager
            .MarkNotificationAsReadAsync(notificationId);
    }

    public async Task SendNotification(NotificationDto notification)
    {
        await Clients.All.SendAsync("ReceiveNotification", notification);
    }
}