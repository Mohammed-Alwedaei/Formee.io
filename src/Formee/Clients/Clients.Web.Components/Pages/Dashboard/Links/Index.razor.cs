using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Dtos.Links;
using Client.Web.Utilities.Models;
using Client.Web.Utilities.Services;

namespace Clients.Web.Components.Pages.Dashboard.Links;

[Route(Routes.Links)]
public partial class Index
{
    [Inject]
    public LinksService LinksService { get; set; }

    [Inject]
    public ContainersService ContainersService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "containerId")]
    public string ContainerId { get; set; }

    private List<ContainerDto> _container = new();

    private List<LinkDto> _links = new();
    private List<LinkHitDto> _linkHits = new();
    private List<ChartModel> _linkHitsChart = new();

    private bool _isSuccessLinkFetch;

    protected override async Task OnParametersSetAsync()
    {
        var userId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        await AggregateLinksPageData(userId);

        await InvokeAsync(StateHasChanged);
    }

    private async Task HandleContainerChangeEvent(string containerId)
    {
        NavigationManager.NavigateTo($"{Routes.Links}?containerId={containerId}");
        _links = await LinksService.GetAllAsync(containerId);

        if (_links.Any())
        {
            _linkHits = await LinksService.GetLinkHitsById(_links.FirstOrDefault().Id);

            if (_linkHits.Any())
            {
                _linkHitsChart = LinksService.GenerateChartDataSeries(_linkHits);
            }
        }
    }

    private async Task AggregateLinksPageData(Guid userId)
    {
        //Get a list of containers
        await ContainersService.GetAllByUserIdAsync(userId);

        //Check if the containers collection is not empty and fetch user links and insights
        if (ContainersService.Containers.Any())
        {
            if (string.IsNullOrEmpty(ContainerId))
            {
                var containerId = ContainersService.Containers.FirstOrDefault().Id;
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
