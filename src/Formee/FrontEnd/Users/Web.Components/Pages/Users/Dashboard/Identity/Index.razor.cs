using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Authorization;

namespace Clients.Web.Components.Pages.Users.Dashboard.Identity;

[Route(Routes.Identity)]
[Authorize]
public partial class Index : IDisposable
{
    [Inject]
    public IdentityService IdentityService { get; set; }

    [Inject]
    public AppStateService AppState { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public SubscriptionsService SubscriptionsService { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "user_id")]
    public string UserId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (AppState.Identity.User.Id == Guid.Empty || AppState.Identity.User.Id != Guid.Parse(UserId))
            NavigationManager.NavigateTo($"{Routes.Aggregate}?redirect_to={NavigationManager.Uri}");

        if (string.IsNullOrEmpty(UserId))
            NavigationManager.NavigateTo("/error/notfound");

        var subscriptionId = AppState.Identity.User.SubscriptionId;

        await AggregatePageData(Guid.Parse(UserId), subscriptionId);
    }

    private async Task AggregatePageData(Guid userId, int subcriptionId)
    {
        AppState.Identity.StateChanged += StateHasChanged;
        await IdentityService.GetByIdAsync(userId);
        await SubscriptionsService.GetUserSubscriptionAsync(subcriptionId);
    }

    public void Dispose()
    {
        AppState.Identity.StateChanged -= StateHasChanged;
    }
}
