using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Clients.Web.Components.Pages.Aggregate;

public partial class Aggregate
{
    [Inject]
    public IdentityService IdentityService { get; set; }
    
    [Inject]
    public AppStateService AppState { get; set; }
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    [CascadingParameter]
    private Task<AuthenticationState> authenticationState { get; set; }

    [Parameter] 
    [SupplyParameterFromQuery(Name = "redirect_to")]
    public string RedirectUrl { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var authState = await authenticationState;

        if (authState.User.Identity.IsAuthenticated)
        {
            var authProviderId = authState.User
                .Claims
                .FirstOrDefault(c => c.Type == "sub")
                ?.Value;
            
            await IdentityService.GetByAuthIdAsync(authProviderId);

            if (AppState.Identity.User is null && AppState.Identity.User.Id == Guid.Empty)
                NavigationManager.NavigateTo($"{Routes.IdentityUpsert}?upsert_type=create&auth_provider_id={authProviderId}");
            else
                NavigationManager.NavigateTo($"{RedirectUrl}?user_id={AppState.Identity.User.Id}");
        }
    }
}