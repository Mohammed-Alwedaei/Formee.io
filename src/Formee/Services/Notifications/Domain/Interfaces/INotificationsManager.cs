namespace Domain.Interfaces;

public interface INotificationsManager
{
    Task<List<NotificationEntity>> GetAllNotificationsAsync
        (Guid globalUserId, int numberOfRecords);

    Task CreateAndSendAsync(NotificationDto notification);

    Task<NotificationEntity> MarkNotificationAsReadAsync
        (int notificationId);
}
