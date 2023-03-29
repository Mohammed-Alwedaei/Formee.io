using Client.Web.Utilities.Dtos.Analytics;
using System.Net.Http.Json;

namespace Client.Web.Utilities.Services;

public class AnalyticsService
{
    private readonly HttpClient _httpClient;

    public AnalyticsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SiteDto> GetSiteByIdAsync(int id)
    {
        var url = $"/api/sites/{id}";

        var response = await _httpClient
            .GetFromJsonAsync<SiteDto>(url);

        if (response is not null)
        {
            return response;
        }

        return new SiteDto();
    }

    public async Task<List<SiteDto>> GetAllSitesAsync(string containerId)
    {
        var url = $"/api/sites/all/{containerId}";

        var response = await _httpClient
            .GetFromJsonAsync<List<SiteDto>>(url);

        return response ?? new List<SiteDto>();
    }

    public async Task<List<PageHitDto>> GetAllHitsInTimePeriodAsync
        (int siteId, DateTime startDate, DateTime endDate)
    {
        var url = $"/api/hits/all/{siteId}/{startDate.ToString("yyyy-MM-dd")}/{endDate.ToString("yyyy-MM-dd")}";

        var response = await _httpClient
            .GetFromJsonAsync<List<PageHitDto>>(url);

        return response ?? new List<PageHitDto>();
    }
}
