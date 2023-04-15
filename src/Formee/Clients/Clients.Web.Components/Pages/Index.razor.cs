using Client.Web.Utilities.Services;

namespace Clients.Web.Components.Pages;

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
    public AppStateService AppStateService { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var today = DateTime.Now;
        var lastWeek = today.AddDays(-7);

        AppStateService.OnUserStateChange += async () =>
        {
            ContainersService.OnChange += async () => await InvokeAsync(StateHasChanged);

            await ContainersService.GetAllByUserIdAsync(AppStateService.User.Id);

            await AnalyticsService.GetAllHitsInTimePeriodAsync(3, lastWeek, today, "categories");
            
            AnalyticsService.GenerateTopPerformingCategories();

            AnalyticsService.GenerateChartDataSeries();
            
            await InvokeAsync(StateHasChanged);
        };
    }

    /// <summary>
    /// Mark a notification as read through SignalR connection
    /// </summary>
    /// <param name="id"></param>
    private async Task HandleMarkAsRead(int id)
    {
        await NotificationsService.MarkNotificationAsReadAsync(id);
    }

    public void Dispose()
    {
        AppStateService.OnUserStateChange -= StateHasChanged;
        ContainersService.OnChange -= StateHasChanged;
        NotificationsService.StateChanged -= StateHasChanged;
    }
}
