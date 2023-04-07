using Client.Web.Utilities.Dtos.History;
using System.Net.Http.Json;

namespace Client.Web.Utilities.Services;

public class HistoryService
{
    private readonly HttpClient _httpClient;

    public HistoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<HistoryDto>> GetHistoryByUserId(Guid userId)
    {
        var url = $"/api/history/all/{userId}";

        var response = await _httpClient
            .GetFromJsonAsync<List<HistoryDto>>(url);

        return response is null ? new List<HistoryDto>() : response;
    }
}
