using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Authorization;

namespace Clients.Web.Components.Pages.Users.Dashboard;

[Authorize]
public partial class Index : IDisposable
{
    [Inject]
    public ContainersService ContainersService { get; set; }

    [Inject]
    public NotificationsService NotificationsService { get; set; }

    [Inject]
    public AnalyticsService AnalyticsService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IdentityService IdentityService { get; set; }

    [Inject]
    public AppStateService AppState { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var today = DateTime.Now;
        var lastWeek = today.AddDays(-7);

        AggregatePageData(lastWeek, today);
    }

    /// <summary>
    /// Mark a notification as read through SignalR connection
    /// </summary>
    /// <param name="id"></param>
    private async Task HandleMarkAsRead(int id)
    {
        await NotificationsService.MarkNotificationAsReadAsync(id);
    }

    private void AggregatePageData(DateTime startDate, DateTime endDate)
    {
        AppState.Identity.StateChanged += async () =>
        {
            AppState.Containers.StateChanged += async () => await InvokeAsync(StateHasChanged);
            AppState.Analytics.StateChanged += async () => await InvokeAsync(StateHasChanged);

            await ContainersService.GetAllByUserIdAsync(AppState.Identity.User.Id);

            await AnalyticsService.GetAllHitsInTimePeriodAsync(3, startDate, endDate, "categories");

            await AnalyticsService.GetTopPerformingCategories(3);

            await InvokeAsync(StateHasChanged);
        };
    }

    public void Dispose()
    {
        AppState.Identity.StateChanged -= StateHasChanged;
        AppState.Containers.StateChanged -= StateHasChanged;
        AppState.Analytics.StateChanged -= StateHasChanged;
        AppState.Notifications.StateChanged -= StateHasChanged;
    }
}
