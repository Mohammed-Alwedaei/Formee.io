using Client.Web.Utilities.Dtos.Identity;
using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace Clients.Web.Components.Pages.Users.Dashboard.Identity;

[Route("/dashboard/identity/upsert")]
[Authorize]
public partial class Upsert : IDisposable
{
    [Inject]
    public IdentityService IdentityService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public AppStateService AppState { get; set; }

    [Inject]
    public ILogger<Upsert> Logger { get; set; }

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

    private UpdateUserDto User { get; set; } = new();

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

        if (Guid.Parse(UserId) != Guid.Empty)
        {
            await IdentityService.GetByIdAsync(Guid.Parse(UserId));

            User = new UpdateUserDto
            {
                Id = AppState.Identity.User.Id,
                Email = AppState.Identity.User.Email,
                UserName = AppState.Identity.User.UserName,
                FirstName = AppState.Identity.User.FirstName,
                LastName = AppState.Identity.User.LastName,
                AuthId = AppState.Identity.User.AuthId,
                Job = AppState.Identity.User.Job,
                Bio = AppState.Identity.User.Bio,
                PhoneNumber = AppState.Identity.User.PhoneNumber,
                BirthDate = AppState.Identity.User.BirthDate,
            };
        }
        else
        {
            User = new UpdateUserDto();
        }

        await InvokeAsync(StateHasChanged);
    }

    private void HandleValidFormSubmit()
    {
        _isVisibleConfirmationDialog = true;

        StateHasChanged();
    }

    private async Task HandleActionApprove()
    {
        _isSendingRequest = true;

        if(UpsertType is "update")
            await IdentityService.UpdateAsync(User);
        else
            await IdentityService.CreateAsync(User);
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
