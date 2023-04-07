using Application.Hubs;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.DbContexts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ServiceBus.Extensions;
using ServiceBus.Models;

namespace Infrastructure.Repositories;

public class NotificationsManager : NotificationRepository<NotificationModel>, INotificationsManager
{
    private readonly ApplicationDbContext _context;
    private readonly IHubContext<NotificationsHub> _notificationsHub;

    public NotificationsManager(ApplicationDbContext context, 
        IHubContext<NotificationsHub> notificationsHub)
    {
        _context = context;
        _notificationsHub = notificationsHub;
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

    public async Task<NotificationEntity> MarkNotificationAsReadAsync
        (int notificationId)
    {
        var notificationToUpdate = await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == notificationId);

        if(notificationToUpdate is null) return new NotificationEntity ();

        notificationToUpdate.IsRead = true;

        _context.Notifications.Update(notificationToUpdate);

        await _context.SaveChangesAsync();

        return notificationToUpdate;
    }

    public override async Task CreateAndSendAsync<TNotificationEntity>(NotificationModel entity)
    {
        await _context.Notifications.AddAsync(entity);

        await _context.SaveChangesAsync();

        await _notificationsHub.Clients.All
            .SendAsync("ReceiveNotification", entity);
    }
}
