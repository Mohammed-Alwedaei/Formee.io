using Client.Web.Utilities.Dtos.Subscriptions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

namespace Client.Web.Utilities.Services;

public class SubscriptionsService : BaseService
{
    private readonly AppStateService _appState;
    private readonly NavigationManager _navigationManager;

    public SubscriptionsService(IHttpClientFactory httpClient,
        AppStateService appState,
        IConfiguration configuration, 
        NavigationManager navigationManager) : base(httpClient, configuration, appState)
    {
        _appState = appState;
        _navigationManager = navigationManager;
    }

    public async Task GetUserSubscriptionAsync(int id)
    {
        try
        {
            var url = $"/gateway/subscriptions/{id}";
            var client = await HttpClient();

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var subscription = await response.Content
                    .ReadFromJsonAsync<SubscriptionDto>();

                _appState.Subscriptions.SetSubscriptionState(subscription);
            }    
                
            else
                throw new Exception();
        }
        catch (Exception ex)
        {
            _navigationManager.NavigateTo("/dashboard/error?error_code=500&error_message=something_wrong");
        }
    }
}
