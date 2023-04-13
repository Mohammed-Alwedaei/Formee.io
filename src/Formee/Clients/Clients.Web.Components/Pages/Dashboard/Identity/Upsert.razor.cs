using Client.Web.Utilities.Dtos.Identity;
using Client.Web.Utilities.Services;

namespace Clients.Web.Components.Pages.Dashboard.Identity;

[Route("/dashboard/identity/upsert")]
public partial class Upsert
{
    [Inject]
    public IdentityService IdentityService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "userId")]
    public string UserId { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "type")]
    public string UpsertType { get; set; }
    
    [Parameter]
    [SupplyParameterFromQuery(Name = "auth_provider_id")]
    public string AuthProviderId { get; set; }

    private UserDto User { get; set; } = new();

    private bool _isVisibleConfirmationDialog;

    private bool _isSendingRequest;

    private bool _isSuccessRequest;

    private bool _isDeleteUpsert;

    protected override void OnParametersSet()
    {
        _isDeleteUpsert = false;
        User.AuthId = AuthProviderId;
    }

    private void HandleValidFormSubmit()
    {
        _isVisibleConfirmationDialog = true;

        User.SubscriptionId = 4;
        User.AvatarId = 4;
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

            NavigationManager.NavigateTo($"/dashboard/identity?userId={createdUser.Id}");
        }
    }

    private void HandleActionReject()
    {
        _isVisibleConfirmationDialog = false;
    }
}
