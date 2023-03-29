using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Models;
using Client.Web.Utilities.Services;
using Syncfusion.Blazor.SplitButtons;

namespace Clients.Web.Components.Pages.Dashboard.Containers;

[Route(Routes.Containers)]
public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public ContainersService ContainersService { get; set; }

    [Inject]
    public AnalyticsService AnalyticsService { get; set; }

    protected List<ContainerDto> _containers { get; set; }

    private ContainersViewModel ViewModel { get; set; }

    protected bool IsFetching { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        IsFetching = true;

        var userId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        _containers = await ContainersService.GetAllByUserIdAsync(userId);

        ViewModel = new ContainersViewModel
        {
            Containers = _containers,
            Sites = new List<SiteDto>()
        };

        foreach (var container in _containers)
        {
            ViewModel.Sites.AddRange(await AnalyticsService
                .GetAllSitesAsync(container.Id));
        }

        IsFetching = false;
    }

    public void NavigateToNewContainer()
    {
        NavigationManager.NavigateTo($"{Routes.UpsertContainer}?type=create");
    }

    public void HandleContainerClick(string containerId)
    {
        NavigationManager.NavigateTo($"{Routes.ViewContainer}?id={containerId}");
    }

    private void ItemSelected(MenuEventArgs args)
    {
        if (args.Item.Text == "Edit")
        {
            NavigationManager.NavigateTo($"{Routes.UpsertContainer}?type=update&containerId={args.Item.Id}");
        }
        else
        {
            NavigationManager.NavigateTo($"{Routes.UpsertContainer}?type=delete&containerId={args.Item.Id}");
        }
    }
}
