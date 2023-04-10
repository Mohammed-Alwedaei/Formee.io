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

    private string _containerId = string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        var userId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        await FetchUserContainers(userId);

        await FetchUserLinks(_containerId);
    }

    private async Task HandleContainerChangeEvent(string containerId)
    {
        NavigationManager.NavigateTo($"{Routes.Links}?containerId={containerId}");
        await FetchUserLinks(containerId);
        _containerId = containerId;
    }

    private async Task FetchUserContainers(Guid userId)
    {
        await ContainersService.GetAllByUserIdAsync(userId);
    }

    private async Task FetchUserLinks(string containerId)
    {
        if (string.IsNullOrEmpty(containerId))
        {
            var firstContainer = ContainersService.Containers.FirstOrDefault();
            await LinksService.GetAllAsync(firstContainer.Id);
        }
        else
        {
            await LinksService.GetAllAsync(containerId);
        }

        await InvokeAsync(StateHasChanged);
    }
}
