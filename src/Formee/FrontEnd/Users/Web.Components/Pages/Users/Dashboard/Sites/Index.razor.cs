using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Authorization;
using Syncfusion.Blazor.SplitButtons;

namespace Clients.Web.Components.Pages.Users.Dashboard.Sites;

[Route(Routes.Sites)]
[Authorize]
public partial class Index
{
    [Inject]
    public AppStateService AppState { get; set; }

    [Inject]
    public AnalyticsService AnalyticsService { get; set; }

    [Inject]
    public ContainersService ContainersService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "user_id")]
    public string UserId { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "container_id")]
    public string ContainerId { get; set; }

    private List<SiteDto> Sites { get; set; }

    /// <summary>
    /// Fetch all sites in a container by id
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        AppState.Containers.StateChanged += async () => await InvokeAsync(StateHasChanged);

        await ContainersService.GetAllByUserIdAsync(Guid.Parse(UserId));

        string currentContainerId;

        if (string.IsNullOrEmpty(ContainerId))
            currentContainerId = AppState.Containers.ContainersCollection.FirstOrDefault().Id;
        else
            currentContainerId = ContainerId;

        AppState.Analytics.StateChanged += async () => await InvokeAsync(StateHasChanged);

        await AnalyticsService.GetAllSitesAsync(currentContainerId);
    }

    /// <summary>
    /// Detect the event sent by the user and redirect to the corresponding upsert page
    /// </summary>
    /// <param name="args"></param>
    private void ItemSelected(MenuEventArgs args)
    {
        NavigationManager.NavigateTo(args.Item.Text == "Edit"
            ? $"{Routes.UpsertContainer}?user_id={UserId}&upsert_type=update&container_id={args.Item.Id}"
            : $"{Routes.UpsertContainer}?user_id={UserId}&upsert_type=delete&container_id={args.Item.Id}");
    }

    /// <summary>
    /// Redirect to /dashboard/sites/view
    /// </summary>
    /// <param name="id"></param>
    private void HandleSiteClick(int id)
    {
        var url = $"{Routes.ViewSites}?user_id={UserId}&site_id={id}";

        NavigationManager.NavigateTo(url);
    }

    /// <summary>
    /// Change route prarameters value
    /// </summary>
    /// <param name="containerId"></param>
    /// <returns></returns>
    private async Task HandleContainerChangeEvent(string? containerId)
    {
        NavigationManager.NavigateTo($"{Routes.Sites}?user_id={UserId}&container_id={containerId}");
    }

    /// <summary>
    /// Redirect users to /dashboard/sites/upsert
    /// </summary>
    private void NavigateToUpsertSite()
    {
        NavigationManager.NavigateTo($"{Routes.UpsertSites}?upsert=create&containerId={ContainerId}");
    }
}
