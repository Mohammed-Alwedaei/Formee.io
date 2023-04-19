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

            NavigationManager.NavigateTo($"{RedirectUrl}?user_id={AppState.Identity.User.Id}");
        }
    }
}