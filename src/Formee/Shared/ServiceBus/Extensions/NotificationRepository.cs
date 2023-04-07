namespace ServiceBus.Extensions;

public abstract class NotificationRepository<TEntity> where TEntity : class
{
    public abstract Task CreateAndSendAsync<TNotificationEntity>(TEntity entity);
}