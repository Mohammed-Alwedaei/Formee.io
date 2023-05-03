using Client.Web.Utilities.Dtos.Forms;
using Client.Web.Utilities.Services;

namespace Clients.Web.Components.Pages.Users.Dashboard.Forms;

[Route(Routes.Forms)]
public partial class Index : IDisposable
{
    [Inject]
    public AppStateService AppState { get; set; }

    [Inject]
    public AnalyticsService AnalyticsService { get; set; }
    
    [Inject]
    public ContainersService ContainersService { get; set; }

    [Inject]
    public FormsService FormsService { get; set; }
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "user_id")]
    public string UserId { get; set; }
    
    [Parameter]
    [SupplyParameterFromQuery(Name = "container_id")]
    public string ContainerId { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "site_id")]
    public int SiteId { get; set; }

    private FormDto _formDto;

    private bool _isVisibleUpsertDialog;

    protected override async Task OnParametersSetAsync()
    {
        var parsedUserId = Guid.Parse(UserId);

        await AggregatePageData(parsedUserId, "64499682bc3a36b9a7ae3ded", SiteId);

        _formDto = new FormDto
        {
            UserId = parsedUserId,
            SiteId = SiteId,
            Details = new DetailsDto()
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerId"></param>
    /// <param name="siteId"></param>
    /// <returns></returns>
    private async Task AggregatePageData
        (Guid userId, string containerId, int siteId)
    {
        await ContainersService.GetAllByUserIdAsync(userId);

        AppState.Containers.StateChanged += async () =>
            await InvokeAsync(StateHasChanged);

        AppState.Analytics.StateChanged += async () =>
            await InvokeAsync(StateHasChanged);

        await AnalyticsService.GetAllSitesAsync(containerId);
        await FormsService.GetAllBySiteIdAsync(siteId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="siteId"></param>
    private void HandleSiteIdChange(string siteId)
    {
        SiteId = int.Parse(siteId);
        StateHasChanged();
    }

    private void HandleUpsertDialogRevealEvent() => 
        _isVisibleUpsertDialog = !_isVisibleUpsertDialog;

    public void Dispose()
    {
        AppState.Containers.StateChanged -= StateHasChanged;
        AppState.Analytics.StateChanged -= StateHasChanged;
    }
}
