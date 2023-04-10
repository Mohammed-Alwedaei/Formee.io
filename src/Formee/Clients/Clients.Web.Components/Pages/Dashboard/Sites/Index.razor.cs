using Client.Web.Utilities.Services;

namespace Clients.Web.Components.Pages.Dashboard.Sites;

[Route(Routes.Sites)]
public partial class Index
{
    [Inject]
    public AnalyticsService AnalyticsService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "containerId")]
    public string ContainerId { get; set; }

    private List<SiteDto> Sites { get; set; }

    private bool IsFetching { get; set; } = true;

    protected override async Task OnParametersSetAsync()
    {
        if (ContainerId is null)
        {
            NavigationManager.NavigateTo(Routes.Containers);
        }
        else
        {
            Sites = await AnalyticsService.GetAllSitesAsync(ContainerId);

            IsFetching = false;
        }
    }

    private void NavigateToUpsertSite()
    {
        NavigationManager.NavigateTo($"{Routes.UpsertSites}?upsert=create&containerId={ContainerId}");
    }
}
