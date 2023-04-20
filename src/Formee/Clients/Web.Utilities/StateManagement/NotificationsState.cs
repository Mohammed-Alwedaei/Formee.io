namespace Client.Web.Utilities.StateManagement;

public class NotificationsState
{
    public List<NotificationDto> NotificationsCollection;

    public int UnreadNotificationsCount;

    public bool IsFetching;
    public bool IsConnected;

    public event Action StateChanged;

    public NotificationsState()
    {
        NotificationsCollection = new List<NotificationDto>();
    }
    
    public void AddToNotificationsCollection(NotificationDto notification)
    {
        NotificationsCollection.Add(notification);
        StateChanged.Invoke();
    }
    
    public void RemoveFromNotificationsCollection(NotificationDto notification)
    {
        NotificationsCollection.Remove(notification);
        StateChanged.Invoke();
    }

    public void SortNotificationsCollection()
    {
        NotificationsCollection.OrderByDescending(n => n.CreatedDate);
        StateChanged.Invoke();
    }

    public void SetNotificationCollectionState(List<NotificationDto> notifications)
    {
        NotificationsCollection = notifications;

        UnreadNotificationsCount = NotificationsCollection.Count(n => n.IsRead != true);
        
        StateChanged.Invoke();
    }

    public void InvertFetchingState()
    {
        IsFetching = !IsFetching;
        StateChanged.Invoke();
    }

    public void SetConnectionState(bool connectionState)
    {
        IsConnected = connectionState;
        StateChanged.Invoke();
    }
}