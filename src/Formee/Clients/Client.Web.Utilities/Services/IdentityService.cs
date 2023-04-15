using Client.Web.Utilities.Dtos.Identity;
using System.Net.Http.Json;

namespace Client.Web.Utilities.Services;
public class IdentityService
{
    private readonly HttpClient _httpClient;
    private readonly AppStateService _appStateService;

    public IdentityService(HttpClient httpClient, AppStateService appStateService)
    {
        _httpClient = httpClient;
        _appStateService = appStateService;
    }

    public async Task<UserDto?> GetByAuthIdAsync(string authProviderId)
    {
        var url = $"/api/identity/users/{authProviderId}";

        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        return new UserDto();
    }
    
    public async Task<UserDto?> GetByIdAsync(Guid userId)
    {
        var url = $"/api/identity/users/{userId}";

        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        return new UserDto();
    }

    public async Task<UserDto?> CreateAsync(UserDto user)
    {
        const string url = "/api/identity/users";

        var response = await _httpClient.PostAsJsonAsync(url, user);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        return new UserDto();
    }
}
