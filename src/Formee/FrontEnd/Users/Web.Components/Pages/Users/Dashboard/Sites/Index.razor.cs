using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Authorization;

namespace Clients.Web.Components.Pages.Users.Dashboard.Sites;

[Route(Routes.Sites)]
[Authorize]
public partial class Index
{
    [Inject]
    public AnalyticsService AnalyticsService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "container_id")]
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
            await AnalyticsService.GetAllSitesAsync(ContainerId);

            IsFetching = false;
        }
    }

    private void NavigateToUpsertSite()
    {
        NavigationManager.NavigateTo($"{Routes.UpsertSites}?upsert=create&containerId={ContainerId}");
    }
}
