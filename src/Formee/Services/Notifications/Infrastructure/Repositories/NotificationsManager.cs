using Application.Hubs;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.DbContexts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NotificationsManager : INotificationsManager
{
    private readonly ApplicationDbContext _context;
    private readonly IHubContext<NotificationsHub> _hub;

    public NotificationsManager(ApplicationDbContext context, IHubContext<NotificationsHub> hub)
    {
        _context = context;
        _hub = hub;
    }

    public async Task<List<NotificationEntity>> GetAllNotificationsAsync
        (Guid globalUserId, int numberOfRecords)
    {
        return await _context.Notifications
            .Where(n => n.GlobalUserId == globalUserId)
            .OrderByDescending(n => n.CreatedDate)
            .Take(numberOfRecords)
            .ToListAsync();
    }

    public async Task CreateAndSendAsync(NotificationDto entity)
    {
        var notification = await _context
            .Notifications.AddAsync(entity);

        await _context.SaveChangesAsync();

        await _hub.Clients.All.SendAsync("ReceiveNotification", notification.Entity);
    }

    public async Task<NotificationEntity> MarkNotificationAsReadAsync
        (int notificationId)
    {
        var notificationToUpdate = await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == notificationId);

        if(notificationToUpdate is null) return new NotificationEntity ();

        notificationToUpdate.IsRead = true;

        _context.Notifications.Update(notificationToUpdate);

        await _context.SaveChangesAsync();

        await _hub.Clients.All.SendAsync("MarkNotificationAsReadResponse", notificationToUpdate);

        return notificationToUpdate;
    }
}
