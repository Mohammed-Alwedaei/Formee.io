using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Services;

namespace Clients.Web.Components.Pages;
public partial class Index: IDisposable
{
    [Inject]
    public ContainersService ContainersService { get; set; }

    [Inject]
    public NotificationsService NotificationsService { get; set; }

    [Inject]
    public AnalyticsService AnalyticsService { get; set; }

    private List<ContainerDto> _containers = new ();

    protected override async Task OnInitializedAsync()
    {
        var userId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        await ContainersService.GetAllByUserIdAsync(userId);

        NotificationsService.ListenToMarkAsRead();

        NotificationsService.OnChange += async () =>
        {
            await InvokeAsync(StateHasChanged);
        };

        var today = DateTime.Now;
        var lastWeek = today.AddDays(-7);

        await AnalyticsService
            .GetAllHitsInTimePeriodAsync(3, lastWeek, today);

        AnalyticsService.GenerateChartDataSeries();

        await InvokeAsync(StateHasChanged);
    }

    private async Task HandleMarkAsRead(int id)
    {
        await NotificationsService.MarkNotificationAsReadAsync(id);
    }

    public void Dispose()
    {
        NotificationsService.OnChange -= StateHasChanged;
    }
}
