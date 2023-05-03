using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Dtos.Links;
using Client.Web.Utilities.Models;
using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Authorization;

namespace Clients.Web.Components.Pages.Users.Dashboard.Links;

[Route(Routes.Links)]
[Authorize]
public partial class Index : IDisposable
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
    [SupplyParameterFromQuery(Name = "user_id")]
    public string? UserId { get; set; }
    
    [Parameter]
    [SupplyParameterFromQuery(Name = "container_id")]
    public string? ContainerId { get; set; }

    private List<LinkDto> _links = new();
    private List<LinkHitDto> _linkHits = new();
    private List<DateChartModel> _linkHitsChart = new();

    private bool _isVisibleUpsertDialog;

    private LinkDto _linkDto = new();

    private readonly string[] _palettes = { "#9e8ddf" };

    protected override async Task OnParametersSetAsync()
    {
        var userId = Guid.Parse(UserId);

        AppState.Containers.StateChanged += StateHasChanged;

        await AggregateContainersPageData(userId);

        AppState.Links.StateChanged += StateHasChanged;

        await AggregateLinksPageData(userId);
    }

    private async Task HandleContainerChangeEvent(string? containerId)
    {
        NavigationManager.NavigateTo($"{Routes.Links}?user_id={UserId}&container_id={containerId}");

        await LinksService.GetAllAsync(containerId);

        var today = DateTime.Now;
        var lastWeek = today.AddDays(-7);
        await LinksService.GetAllHitsByContainerId(containerId, lastWeek, today);
    }

    private async Task AggregateContainersPageData(Guid userId)
    {
        //Get a list of containers
        await ContainersService.GetAllByUserIdAsync(userId);
    }

    private async Task AggregateLinksPageData(Guid userId)
    {
        //Check if the containers collection is not empty and fetch user links and insights
        if (AppState.Containers.ContainersCollection is not null && 
            AppState.Containers.ContainersCollection.Any())
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
    }

    private void HandleNewContainerClick(string containerId)
    {
        _linkDto.ContainerId = containerId;
        _linkDto.UserId = Guid.Parse(UserId);

        _linkDto.LinkDetails = new LinkDetailsDto();

        _isVisibleUpsertDialog = true;
    }

    private async Task HandleNewLinkApprove()
    {
        _linkDto.LinkDetails.Name = _linkDto.Name;

        await LinksService.CreateAsync(_linkDto);
    }
    
    private void HandleNewLinkReject()
    {
        _isVisibleUpsertDialog = false;
    }

    private async void HandleLinkHitsRefresh(string containerId)
    {
        var today = DateTime.Now;
        var lastWeek = today.AddDays(-7);

        await LinksService.GetAllHitsByContainerId(containerId, lastWeek, today);
    }
    
    private async Task HandleLinksRefresh(string containerId)
    {
        await LinksService.GetAllAsync(containerId);
    }

    private void HandleLinkItemClick(int linkId)
    {
        var route = $"{Routes.LinksView}?user_id={UserId}&container_id={ContainerId}&link_id={linkId}";

        NavigationManager.NavigateTo(route);
    }

    public void Dispose()
    {
        AppState.Containers.StateChanged -= StateHasChanged;
        AppState.Links.StateChanged -= StateHasChanged;
    }
}
