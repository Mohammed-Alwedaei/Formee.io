using Client.Web.Utilities.Dtos.History;
using System.Net.Http.Json;

namespace Client.Web.Utilities.Services;

public class HistoryService
{
    private readonly HttpClient _httpClient;
    private readonly AppStateService _appState;

    public HistoryService(HttpClient httpClient, AppStateService appState)
    {
        _httpClient = httpClient;
        _appState = appState;
    }

    public async Task GetHistoryByUserId(Guid userId, int pageNumber, int recordPerPage)
    {
        var url = $"/api/history/all/{userId}?pageNumber={pageNumber}&nPerPage={recordPerPage}";

        var historyCollection = await _httpClient
            .GetFromJsonAsync<List<HistoryDto>>(url);
        
        _appState.SetHistoryState(historyCollection);
    }
}