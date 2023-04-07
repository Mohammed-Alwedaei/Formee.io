using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Dtos.Links;
using Client.Web.Utilities.Services;

namespace Clients.Web.Components.Pages.Dashboard.Links;

[Route(Routes.Links)]
public partial class Index
{
    [Inject]
    public LinksService LinksService { get; set; }

    [Inject]
    public ContainersService ContainersService { get; set; }

    private IReadOnlyList<LinkDto> Links { get; set; }

    private List<ContainerDto> Containers { get; set; }

    private string ContainerId { get; set; }

    private bool IsFetching { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await FetchPageData(ContainerId);
    }

    private async Task HandleContainerChangeEvent(string containerId)
    {
        ContainerId = containerId;

        await FetchPageData(ContainerId);
    }

    private async Task FetchPageData(string containerId)
    {
        IsFetching = true;
        await InvokeAsync(StateHasChanged);

        var userId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        Containers = await ContainersService.GetAllByUserIdAsync(userId);

        var firstContainer = Containers.FirstOrDefault();

        Links = new List<LinkDto>();

        if (string.IsNullOrEmpty(containerId))
        {
            Links = await LinksService.GetAllAsync(firstContainer.Id);
        }
        else
        {
            Links = await LinksService.GetAllAsync(containerId);
        }

        IsFetching = false;

        await InvokeAsync(StateHasChanged);
    }
}
