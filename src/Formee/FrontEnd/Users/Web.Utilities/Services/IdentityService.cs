using Client.Web.Utilities.Dtos.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Client.Web.Utilities.Services;
public class IdentityService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly AppStateService _appState;
    private readonly ILogger<IdentityService> _logger;

    public IdentityService(HttpClient httpClient,
        IConfiguration configuration,
        AppStateService appState, 
        ILogger<IdentityService> logger)
    {
        _appState = appState;
        _logger = logger;
        _configuration = configuration;
        _httpClient = httpClient;
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

    public async Task<CreateUserDto?> CreateAsync(CreateUserDto user)
    {
        const string url = "/api/identity/users";

        var response = await _httpClient.PostAsJsonAsync(url, user);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<CreateUserDto>();

        return new CreateUserDto();
    }
}
