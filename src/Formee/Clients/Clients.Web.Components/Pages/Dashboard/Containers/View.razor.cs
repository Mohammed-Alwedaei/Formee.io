using Client.Web.Utilities.Constants;
using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Models;
using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Components;

namespace Clients.Web.Components.Pages.Dashboard.Containers;

[Route(Routes.ViewContainer)]
public partial class View
{
    [Parameter]
    [SupplyParameterFromQuery(Name = "id")]
    public string ContainerId { get; set; }

    [Inject]
    public ContainersService ContainersService { get; set; }

    private ContainerDto Container { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        Container = await ContainersService.GetByIdAsync(ContainerId);
    }
}
