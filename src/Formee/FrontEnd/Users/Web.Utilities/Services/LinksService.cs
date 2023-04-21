using Client.Web.Utilities.Dtos.Links;

namespace Client.Web.Utilities.Services;

public class LinksService
{
    private readonly HttpClient _httpClient;
    private readonly AppStateService _appState;

    public LinksService(HttpClient httpClient, AppStateService appState)
    {
        _httpClient = httpClient;
        _appState = appState;
    }

    public async Task GetAllAsync(string? containerId)
    {
        _appState.Links.InvertFetchingState();

        var url = $"/api/links/all/{containerId}";

        var response = await _httpClient
            .GetFromJsonAsync<List<LinkDto>>(url);

        _appState.Links.SetLinksCollectionState(response ?? new List<LinkDto>());

        _appState.Links.InvertFetchingState();
    }

    public async Task GetLinkHitsById(int linkId)
    {
        _appState.Links.InvertFetchingState();

        var url = $"/api/links/redirects/all/{linkId}";

        var response = await _httpClient
            .GetFromJsonAsync<List<LinkHitDto>>(url);

        _appState.Links.SetLinkHitsState(response ?? new List<LinkHitDto>());
        
        _appState.Links.InvertFetchingState();
    }

    public async Task GetAllHitsByContainerId(string? containerId, DateTime startDate, DateTime endDate)
    {
        var formattedStartDate = startDate.ToString("yyyy-MM-dd");
        var formattedEndDate = endDate.AddDays(1)
            .ToString("yyyy-MM-dd");

        var url = $"/api/links/hits/all/{containerId}/{formattedStartDate}/{formattedEndDate}";

        var response = await _httpClient
            .GetFromJsonAsync<List<LinkHitDto>>(url);

        _appState.Links.SetLinkHitsInContainerCollectionState(response ?? new List<LinkHitDto>());
    }

    /*public List GenerateChartDataSeries(List<LinkHitDto> hits)
    {
        IsFetching = true;
        var dataSeries = new List<DateChartModel>();

        foreach (var hit in hits)
        {
            var isAvailableDate = dataSeries
                .FirstOrDefault(c => c.Date.Date == hit.CreatedDate.Date);

            if (isAvailableDate is not null)
            {
                isAvailableDate.Count++;
            }
            else
            {
                dataSeries.Add(new DateChartModel
                {
                    Id = hit.Id,
                    Date = hit.CreatedDate,
                    Count = 1
                });
            }
        }

        IsFetching = false;
        return dataSeries;
    }*/
}
