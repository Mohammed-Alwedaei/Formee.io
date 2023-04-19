using Client.Web.Utilities.Dtos.History;

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
        _appState.History.IsFetching = true;
        
        var url = $"/api/history/all/{userId}?pageNumber={pageNumber}&nPerPage={recordPerPage}";

        var response = await _httpClient
            .GetFromJsonAsync<List<HistoryDto>>(url);

        if (response != null && response.Any())
        {
            _appState.History.HistoryCollection = new List<HistoryDto>();
            _appState.History.HistoryCollection = response;
        }
        else
            _appState.History.HistoryCollection = new List<HistoryDto>();

        _appState.History.IsFetching = false;
    }
}