using Client.Web.Utilities.Dtos.Identity;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace Client.Web.Utilities.Services;
public class IdentityService
{
    private readonly HttpClient _httpClient;
    private readonly AppStateService _appState;
    private readonly ILogger<IdentityService> _logger;

    public IdentityService(HttpClient httpClient, AppStateService appState, ILogger<IdentityService> logger)
    {
        _httpClient = httpClient;
        _appState = appState;
        _logger = logger;
    }

    public async Task GetByAuthIdAsync(string authProviderId)
    {
        _appState.Identity.IsFetching = true;
        
        var url = $"/api/identity/users/{authProviderId}";
        
        _logger.LogInformation("FETCHING: api request to route {url} at {date}", url, DateTime.UtcNow);

        var response = await _httpClient.GetFromJsonAsync<UserDto>(url);

        if (response.Id == Guid.Empty || string.IsNullOrEmpty(response.AuthId))
        {
            _logger.LogError("ERROR: could not fetch route: {url} with status code of {statusCode}", url);

            _appState.Identity.SetUserState(new UserDto());
        }
        
        _logger.LogInformation("SUCCESS: fetch request to route: {url} succeeded", url);
        _appState.Identity.SetUserState(response);
            
        _appState.Identity.IsFetching = false;
    }
    
    public async Task GetByIdAsync(Guid userId)
    {
        var url = $"/api/identity/users/{userId}";

        _logger.LogInformation("FETCHING: api request to route {url} at {date}", url, DateTime.UtcNow);

        var response = await _httpClient.GetFromJsonAsync<UserDto>(url);

        if (response.Id == Guid.Empty || string.IsNullOrEmpty(response.AuthId))
        {
            _logger.LogError("ERROR: could not fetch route: {url} with status code of {statusCode}", url);

            _appState.Identity.SetUserState(response);
        }
        
        _logger.LogInformation("SUCCESS: fetch request to route: {url} succeeded", url);
        _appState.Identity.SetUserState(new UserDto());
            
        _appState.Identity.IsFetching = false;
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
