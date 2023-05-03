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

        var userProfile = "# Hi Im Mohammed Alwedaei\r\n## I like\r\n- Software design and development\r\n- Drinking Coffee \r\n- Writing my daily routine\r\n- Help people learn more\r\n- Watch movies\r\n## Im currently doing my Bachelors degree in\r\n1. Multimedia Systems\r\n   - Web design and programming \r\n   - Multimedia tools\r\n   - Software architecture principles\r\n## Im currently intrested in:\r\n1. Software Architecture and design patterns\r\n   - Software design patterns\r\n   - Software development principles and rules\r\n   - Microservices Architecture";
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
