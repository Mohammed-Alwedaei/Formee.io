using Client.Web.Utilities.Dtos.Links;
using System.Net.Http.Json;

namespace Client.Web.Utilities.Services;

public class LinksService
{
    private readonly HttpClient _httpClient;

    public LinksService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<LinkDto>> GetAllAsync(string containerId)
    {
        var url = $"/api/links/all/{containerId}";

        var response = await _httpClient
            .GetFromJsonAsync<List<LinkDto>>(url);

        return response ?? new List<LinkDto>();
    }
}
