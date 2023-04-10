using Client.Web.Utilities.Dtos;
using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Web.Utilities.Services;

public class NotificationsService : IAsyncDisposable
{
    public List<NotificationDto> Notifications;

    public int AllNotificationsCount;
    public int UnreadNotificationsCount;

    public bool IsFetching;

    private readonly HttpClient _httpClient;
    private HubConnection _hubConnection;
    public bool ConnectionStatus;


    public event Action OnChange;

    public NotificationsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateClientConnection()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/hubs/notifications")
            .Build();

        await _hubConnection.StartAsync();
    }

    public async Task GetAllByUserIdAsync
        (Guid globalUserId, int numberOfRecords)
    {
        IsFetching = true;

        var url = $"/api/notifications/all/{globalUserId}/{numberOfRecords}";

        var response = await _httpClient
            .GetFromJsonAsync<List<NotificationDto>>(url);

        Notifications = new List<NotificationDto>();

        Notifications = response;

        Notifications.OrderByDescending(n => n.CreatedDate);

        UpdateNotificationsMeta();

        IsFetching = false;

        OnChange.Invoke();
    }

    public async Task MarkNotificationAsReadAsync
        (int id)
    {
        await _hubConnection
            .InvokeAsync("MarkNotificationAsReadRequest", id);
    }

    public void ReceiveNotification()
    {
        if (_hubConnection is null)
        {
            return;
        }

        _hubConnection.On<NotificationDto>("ReceiveNotification",
            (notification) => Notifications.Add(notification));
    }

    public void ListenToMarkAsRead()
    {
        if (_hubConnection is null)
        {
            return;
        }

        _hubConnection.On<NotificationDto>("MarkNotificationAsReadResponse",
            (notification) =>
            {
                var notificationToRemove = Notifications
                    .FirstOrDefault(n => n.Id == notification.Id);

                Notifications.Remove(notificationToRemove);

                Notifications.Add(notification);

                var sortedNotifications = Notifications
                    .OrderByDescending(n => n.CreatedDate)
                    .ToList();

                Notifications = new List<NotificationDto>();

                Notifications = sortedNotifications;

                UpdateNotificationsMeta();

                OnChange.Invoke();
            });
    }

    public void GetConnectionStatus()
    {
        ConnectionStatus = _hubConnection?.State == HubConnectionState.Connected;
    }

    private void UpdateNotificationsMeta()
    {
        AllNotificationsCount = Notifications.Count;

        UnreadNotificationsCount = Notifications
            .Count(n => n.IsRead != true);
    }

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
        }
    }
}
