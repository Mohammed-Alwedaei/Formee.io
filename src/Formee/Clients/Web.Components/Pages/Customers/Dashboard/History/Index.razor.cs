using Client.Web.Utilities.Services;
using Syncfusion.Blazor.Navigations;

namespace Clients.Web.Components.Pages.Customers.Dashboard.History;

[Route(Routes.History)]
public partial class Index : IDisposable
{
    [Inject]
    public HistoryService HistoryService { get; set; }

    [Inject]
    public AppStateService AppState { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "user_id")]
    public string UserId { get; set; }

    private SfPager _pager;

    private int _skipValue;

    private int _recordPerPage;

    protected override async Task OnParametersSetAsync()
    {
        _skipValue = 0;

        AppState.History.StateChanged += async () =>
        {
            await InvokeAsync(StateHasChanged);
        };

        await HistoryService.GetHistoryByUserId(Guid.Parse(UserId), _skipValue, 5);
    }

    private async Task HandlePageChange(PagerItemClickEventArgs args)
    {
        _skipValue = args.CurrentPage * _pager.PageSize - _pager.PageSize - 1;
        _recordPerPage = _pager.PageSize;
        await HistoryService.GetHistoryByUserId(Guid.Parse(UserId), _skipValue, _recordPerPage);
    }

    public void Dispose()
    {
        AppState.History.StateChanged -= StateHasChanged;
    }
}
