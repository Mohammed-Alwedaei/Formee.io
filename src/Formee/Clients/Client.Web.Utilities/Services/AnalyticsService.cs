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

        if (response is not null)
        {
            return response;
        }

        return new List<SiteDto>();
    }
}
