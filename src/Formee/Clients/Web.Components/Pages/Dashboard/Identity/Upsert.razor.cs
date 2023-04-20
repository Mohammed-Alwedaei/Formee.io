using Client.Web.Utilities.Dtos.Identity;
using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Syncfusion.Blazor.Inputs;

namespace Clients.Web.Components.Pages.Dashboard.Identity;

[Route("/dashboard/identity/upsert")]
public partial class Upsert : IDisposable
{
    [Inject]
    public IdentityService IdentityService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public AppStateService AppState { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState> authenticationState { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "user_id")]
    public string UserId { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "upsert_type")]
    public string UpsertType { get; set; }
    
    [Parameter]
    [SupplyParameterFromQuery(Name = "auth_provider_id")]
    public string AuthProviderId { get; set; }

    private CreateUserDto User { get; set; } = new();

    private bool _isVisibleConfirmationDialog;

    private bool _isSendingRequest;

    private bool _isSuccessRequest;

    private bool _isDeleteUpsert;

    protected override async Task OnParametersSetAsync()
    {
        _isDeleteUpsert = false;
        
        var authState = await authenticationState;
        
        var email = authState.User
                .Claims
                .FirstOrDefault(c => c.Type == "email")
                ?.Value;

        User.Email = email;
        User.AuthId = AuthProviderId;

        AppState.Identity.StateChanged += StateHasChanged;
    }

    private void HandleValidFormSubmit()
    {
        _isVisibleConfirmationDialog = true;

        StateHasChanged();
    }

    private async Task HandleActionApprove()
    {
        _isSendingRequest = true;

        var createdUser = await IdentityService.CreateAsync(User);

        if (createdUser != null)
        {
            _isSendingRequest = false;
            _isSuccessRequest = true;

            await Task.Delay(2500);

            NavigationManager.NavigateTo("/");
        }
    }

    private void HandleActionReject()
    {
        _isVisibleConfirmationDialog = false;
    }
    
    public void Dispose()
    {
        AppState.Identity.StateChanged -= StateHasChanged;
    }
}
