using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Dtos.Links;
using Client.Web.Utilities.Models;
using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Authorization;

namespace Clients.Web.Components.Pages.Users.Dashboard.Links;

[Route(Routes.Links)]
[Authorize]
public partial class Index
{
    [Inject]
    public LinksService LinksService { get; set; }

    [Inject]
    public AppStateService AppState { get; set; }

    [Inject]
    public ContainersService ContainersService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "container_id")]
    public string? ContainerId { get; set; }

    private List<LinkDto> _links = new();
    private List<LinkHitDto> _linkHits = new();
    private List<DateChartModel> _linkHitsChart = new();

    private bool _isSuccessLinkFetch;

    private readonly string[] _palettes = { "#9e8ddf" };

    protected override async Task OnParametersSetAsync()
    {
        var userId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        await AggregateLinksPageData(userId);

        await InvokeAsync(StateHasChanged);
    }

    private async Task HandleContainerChangeEvent(string? containerId)
    {
        NavigationManager.NavigateTo($"{Routes.Links}?container_id={containerId}");
        await LinksService.GetAllAsync(containerId);

        if (_links.Any())
        {
            var today = DateTime.Now;
            var lastWeek = today.AddDays(-7);

            await LinksService.GetAllHitsByContainerId(containerId, lastWeek, today);

            //TODO: create API endpoint to create the chart models
        }
    }

    private async Task AggregateLinksPageData(Guid userId)
    {
        //Get a list of containers
        await ContainersService.GetAllByUserIdAsync(userId);

        //Check if the containers collection is not empty and fetch user links and insights
        if (AppState.Containers.ContainersCollection.Any())
        {
            if (string.IsNullOrEmpty(ContainerId))
            {
                var containerId = AppState.Containers.ContainersCollection.FirstOrDefault().Id;
                await HandleContainerChangeEvent(containerId);
            }
            else
            {
                await HandleContainerChangeEvent(ContainerId);
            }
        }

        _isSuccessLinkFetch = false;
    }
}
