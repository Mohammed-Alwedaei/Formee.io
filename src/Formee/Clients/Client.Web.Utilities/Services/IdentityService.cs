using Client.Web.Utilities.Dtos.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;

namespace Client.Web.Utilities.Services;
public class IdentityService
{
    private readonly HttpClient _httpClient;

    public IdentityService(HttpClient httpClient)
    {
        _httpClient = httpClient;


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
