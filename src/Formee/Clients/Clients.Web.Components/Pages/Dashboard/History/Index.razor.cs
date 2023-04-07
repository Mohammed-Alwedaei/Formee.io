using Client.Web.Utilities.Dtos.History;
using Client.Web.Utilities.Services;

namespace Clients.Web.Components.Pages.Dashboard.History;

[Route(Routes.History)]
public partial class Index
{
    [Inject]
    public HistoryService HistoryService { get; set; }

    private List<HistoryDto> History { get; set; }

    private bool IsFetching { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var userId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        IsFetching = true;

        History = await HistoryService.GetHistoryByUserId(userId);

        IsFetching = false;
    }
}
