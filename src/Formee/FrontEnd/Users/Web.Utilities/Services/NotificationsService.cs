using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace Client.Web.Utilities.Services;

public class NotificationsService : IAsyncDisposable
{
    private readonly HttpClient _httpClient;
    private HubConnection? _hubConnection;
    private readonly AppStateService _appState;
    private readonly IConfiguration _configuration;

    public NotificationsService(HttpClient httpClient, AppStateService appState, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _appState = appState;
        _configuration = configuration;
    }

    public async Task CreateClientConnection()
    {
        var url = _configuration["GatewayUrl"];

        _hubConnection = new HubConnectionBuilder()
            .WithUrl($"{url}/hubs/notifications")
            .Build();

        await _hubConnection.StartAsync();

        _appState.Notifications.SetConnectionState(_hubConnection.State == HubConnectionState.Connected);
    }

    public async Task GetAllByUserIdAsync(Guid userId, int numberOfRecords)
    {
        _appState.Notifications.InvertFetchingState();

        var url = $"/api/notifications/all/{userId}/{numberOfRecords}";

        var response = await _httpClient
            .GetFromJsonAsync<List<NotificationDto>>(url);

        _appState.Notifications.SetNotificationCollectionState(response ?? new List<NotificationDto>());

        _appState.Notifications.InvertFetchingState();
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
            (notification) => _appState.Notifications.AddToNotificationsCollection(notification));
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
                var notificationToRemove = _appState.Notifications.NotificationsCollection
                    .FirstOrDefault(n => n.Id == notification.Id);

                _appState.Notifications.RemoveFromNotificationsCollection(notificationToRemove);

                _appState.Notifications.AddToNotificationsCollection(notification);

                _appState.Notifications.SortNotificationsCollection();
            });
    }

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
        }
    }
}