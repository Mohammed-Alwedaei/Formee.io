using Client.Web.Utilities.Dtos;
using System.Net.Http.Json;

namespace Client.Web.Utilities.Services;

public class NotificationsService
{
    private readonly HttpClient _httpClient;

    public NotificationsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<NotificationDto>> GetAllByUserIdAsync
        (Guid globalUserId, int numberOfRecords)
    {
        var url = $"/api/notifications/all/{globalUserId}/{numberOfRecords}";

        var response = await _httpClient
            .GetFromJsonAsync<List<NotificationDto>>(url);

        return response ?? new List<NotificationDto>();
    }

    public async Task<NotificationDto> MarkNotificationAsReadAsync
        (int notificationId)
    {
        var url = $"/api/notifications/reads/{notificationId}";

        var response = await _httpClient
            .GetFromJsonAsync<NotificationDto>(url);

        return response ?? new NotificationDto();
    }
}
