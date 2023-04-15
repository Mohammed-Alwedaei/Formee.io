using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Services;

namespace Clients.Web.Components.Pages.Dashboard.Containers;

[Route(Routes.ViewContainer)]
public partial class View
{
    [Parameter]
    [SupplyParameterFromQuery(Name = "user_id")]
    public string UserId { get; set; }
    
    [Parameter]
    [SupplyParameterFromQuery(Name = "container_id")]
    public string ContainerId { get; set; }

    private string? _containerId;

    [Inject]
    public ContainersService ContainersService { get; set; }

    private ContainerDto Container { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrEmpty(ContainerId))
        {
            await ContainersService.GetAllByUserIdAsync(Guid.Parse(UserId));
            var container = ContainersService.Containers.FirstOrDefault();
            _containerId = container?.Id;
        }
        else
        {
            _containerId = ContainerId;
        }
        
        await ContainersService.GetByIdAsync(_containerId);
    }

    private void HandleContainerChangeEvent(string containerId)
    {
        _containerId = containerId;
    }
}
