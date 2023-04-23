using Client.Web.Utilities.Dtos.Links;
using Microsoft.Extensions.Configuration;

namespace Client.Web.Utilities.Services;

public class LinksService : BaseService
{
    private readonly AppStateService _appState;

    public LinksService(IHttpClientFactory httpClient,
        AppStateService appState,
        IConfiguration configuration) : base(httpClient, configuration, appState)
    {
        _appState = appState;
    }

    public async Task GetAllAsync(string? containerId)
    {
        _appState.Links.InvertFetchingState();

        var url = $"/api/links/all/{containerId}";

        var client = await HttpClient();

        var response = await client.GetFromJsonAsync<List<LinkDto>>(url);

        _appState.Links.SetLinksCollectionState(response ?? new List<LinkDto>());

        _appState.Links.InvertFetchingState();
    }

    public async Task GetLinkHitsById(int linkId)
    {
        _appState.Links.InvertFetchingState();

        var url = $"/api/links/redirects/all/{linkId}";

        var client = await HttpClient();

        var response = await client.GetFromJsonAsync<List<LinkHitDto>>(url);

        _appState.Links.SetLinkHitsState(response ?? new List<LinkHitDto>());
        
        _appState.Links.InvertFetchingState();
    }

    public async Task GetAllHitsByContainerId(string? containerId, DateTime startDate, DateTime endDate)
    {
        var formattedStartDate = startDate.ToString("yyyy-MM-dd");
        var formattedEndDate = endDate.AddDays(1)
            .ToString("yyyy-MM-dd");

        var url = $"/api/links/hits/all/{containerId}/{formattedStartDate}/{formattedEndDate}";

        var client = await HttpClient();

        var response = await client.GetFromJsonAsync<List<LinkHitDto>>(url);

        _appState.Links.SetLinkHitsInContainerCollectionState(response ?? new List<LinkHitDto>());
    }
}
