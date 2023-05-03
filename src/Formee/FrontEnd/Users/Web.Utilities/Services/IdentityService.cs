using Client.Web.Utilities.Constants;
using Client.Web.Utilities.Dtos.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Client.Web.Utilities.Services;
public class IdentityService : BaseService
{
    private readonly AppStateService _appState;
    private readonly ILogger<IdentityService> _logger;
    private readonly NavigationManager _navigationManager;

    public IdentityService(IHttpClientFactory httpClient,
        IConfiguration configuration,
        AppStateService appState,
        ILogger<IdentityService> logger,
        NavigationManager navigationManager) 
        : base(httpClient, configuration, appState)
    {
        _appState = appState;
        _logger = logger;
        _navigationManager = navigationManager;
    }

    public async Task GetByAuthIdAsync(string authProviderId)
    {
        _appState.Identity.IsFetching = true;

        var url = $"/api/identity/users/{authProviderId}";

        _logger.LogInformation("FETCHING: api request to route {url} at {date}", url, DateTime.UtcNow);

        var client = await HttpClient();

        var response = await client.GetFromJsonAsync<UserDto>(url);

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

        var client = await HttpClient();

        var response = await client.GetFromJsonAsync<UserDto>(url);

        if (response.Id == Guid.Empty || string.IsNullOrEmpty(response.AuthId))
        {
            _logger.LogError("ERROR: could not fetch route: {url} with status code of {statusCode}", url);

            _appState.Identity.SetUserState(new UserDto());
        }

        _logger.LogInformation("SUCCESS: fetch request to route: {url} succeeded", url);
        _appState.Identity.SetUserState(response);

        _appState.Identity.IsFetching = false;
    }

    public async Task CreateAsync(UpdateUserDto user)
    {
        try
        {
            const string url = "/api/identity/users";

            var client = await HttpClient();

            var response = await client.PostAsJsonAsync(url, user);

            if (response.IsSuccessStatusCode)
                _navigationManager.NavigateTo($"/{Routes.Identity}?user_id={_appState.Identity.User.Id}");

            throw new Exception();
        }
        catch (Exception ex)
        {
            _navigationManager.NavigateTo($"/dashboard/error?error_code=500&error_message=something_wrong");
        }
    }

    public async Task UpdateAsync(UpdateUserDto user)
    {
        try
        {
            const string url = "/api/identity/users";
            var client = await HttpClient();

            var response = await client.PutAsJsonAsync(url, user);

            if (response.IsSuccessStatusCode)
                _navigationManager.NavigateTo($"/{Routes.Identity}?user_id={_appState.Identity.User.Id}");
            else
                throw new Exception();
        }
        catch (Exception ex)
        {
            _navigationManager.NavigateTo("/dashboard/error?error_code=500&error_message=something_wrong");
        }
    }
}
