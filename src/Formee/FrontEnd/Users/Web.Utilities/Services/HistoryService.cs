using Client.Web.Utilities.Dtos.History;
using Microsoft.Extensions.Configuration;

namespace Client.Web.Utilities.Services;

public class HistoryService : BaseService
{
    private readonly AppStateService _appState;

    public HistoryService(IHttpClientFactory httpClient,
        AppStateService appState,
        IConfiguration configuration) : base(httpClient, configuration)
    {
        _appState = appState;
    }

    public async Task GetHistoryByUserId(Guid userId, int pageNumber, int recordPerPage)
    {
        _appState.History.IsFetching = true;
        
        var url = $"/api/history/all/{userId}?pageNumber={pageNumber}&nPerPage={recordPerPage}";

        var client = await HttpClient();

        var response = await client.GetFromJsonAsync<List<HistoryDto>>(url);

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