using Client.Web.Utilities.Services;
using Syncfusion.Blazor.Buttons;
using Syncfusion.Blazor.SplitButtons;

namespace Clients.Web.Components.Pages.Dashboard.Containers;

[Route(Routes.Containers)]
public partial class Index : IDisposable
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public ContainersService ContainersService { get; set; }

    [Inject]
    public AnalyticsService AnalyticsService { get; set; }

    [Parameter]
    public string UserId { get; set; }

    private Guid ParsedUserId => new(UserId);

    private string _containerId = string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        ContainersService.OnChange += StateHasChanged;

        await ContainersService.GetAllByUserIdAsync(ParsedUserId);
    }

    public void NavigateToNewContainer()
    {
        NavigationManager.NavigateTo($"{Routes.UpsertContainer}?type=create");
    }

    public void HandleContainerClick(string containerId)
    {
        NavigationManager.NavigateTo($"{Routes.ViewContainer}?id={containerId}");
    }

    private async Task HandleContainerChangeEvent(string containerId)
    {
        _containerId = containerId;
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

    private void HandleSiteSelect(ChipEventArgs args)
    {
        var site = AnalyticsService.Sites
            .FirstOrDefault(s => s.Name == args.Text);

        NavigationManager.NavigateTo($"{Routes.Sites}?containerId={site.ContainerId}");
    }

    public void Dispose()
    {
        ContainersService.OnChange -= StateHasChanged;
    }
}
