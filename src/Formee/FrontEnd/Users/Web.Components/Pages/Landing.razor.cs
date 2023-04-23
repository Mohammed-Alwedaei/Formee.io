using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Clients.Web.Components.Pages;

[Route("/")]
public partial class Landing
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [CascadingParameter]
    public Task<AuthenticationState> AuthenticationState { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "user_id")]
    public string UserId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var authState = await AuthenticationState;

        if (authState.User.Identity.IsAuthenticated == false)
            NavigationManager.NavigateToLogin("/authentication/login");
    }
}
