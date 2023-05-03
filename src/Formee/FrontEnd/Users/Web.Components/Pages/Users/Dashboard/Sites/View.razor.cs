using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Authorization;

namespace Clients.Web.Components.Pages.Users.Dashboard.Sites;

[Authorize]
[Route(Routes.ViewSites)]
public partial class View : IDisposable
{
    [Inject]
    public AppStateService AppState { get; set; }
    
    [Inject]
    public AnalyticsService AnalyticsService { get; set; }

    [Inject]
    public FormsService FormsService { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "user_id")]
    public string UserId { get; set; }
    
    [Parameter]
    [SupplyParameterFromQuery(Name = "container_id")]
    public string ContainerId { get; set; }
    
    [Parameter]
    [SupplyParameterFromQuery(Name = "site_id")]
    public int SiteId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await AggregatePageData(SiteId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="siteId"></param>
    /// <returns></returns>
    private async Task AggregatePageData(int siteId)
    {
        AppState.Analytics.StateChanged += async () => 
            await InvokeAsync(StateHasChanged);
        
        AppState.Forms.StateChanged += async () => 
            await InvokeAsync(StateHasChanged);

        var today = DateTime.Now;
        var lastWeek = today.AddDays(-15);

        await AnalyticsService.GetSiteByIdAsync(siteId);

        await FormsService.GetAllBySiteIdAsync(siteId);

        await AnalyticsService.GetAllHitsInTimePeriodAsync(siteId, 
            lastWeek, 
            today, 
            "modeled");
    }

    public void Dispose()
    {
        AppState.Analytics.StateChanged -= StateHasChanged;
    }
}
