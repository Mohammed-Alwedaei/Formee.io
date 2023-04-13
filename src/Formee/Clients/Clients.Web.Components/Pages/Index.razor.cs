using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Dtos.Identity;
using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

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

    protected UserDto? User;

    private List<ContainerDto> _containers = new();

    protected override async Task OnParametersSetAsync()
    {
        var today = DateTime.Now;
        var lastWeek = today.AddDays(-7);

        try
        {
            await ContainersService.GetAllByUserIdAsync(User.Id);

            await AnalyticsService
                .GetAllHitsInTimePeriodAsync(3, lastWeek, today);

            AnalyticsService.GenerateChartDataSeries();

            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {

        }

    }

    private async Task HandleMarkAsRead(int id)
    {
        await NotificationsService.MarkNotificationAsReadAsync(id);
    }

    public void Dispose()
    {
        NotificationsService.StateChanged -= StateHasChanged;
    }
}
