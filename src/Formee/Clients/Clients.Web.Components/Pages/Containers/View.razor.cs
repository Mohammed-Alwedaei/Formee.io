using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Models;
using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Components;

namespace Clients.Web.Components.Pages.Containers;

public partial class View
{
    [Parameter]
    [SupplyParameterFromQuery(Name = "id")]
    public string ContainerId { get; set; }

    [Inject]
    public ContainersService ContainersService { get; set; }

    private ContainerDto Container { get; set; } = new();

    private List<ChartModel> _pageHitsDisplay { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        Container = await ContainersService.GetByIdAsync(ContainerId);

        _pageHitsDisplay = new(){
            new ChartModel { X = "Bahrain", Y = 170 },
            new ChartModel { X = "Japan", Y = 1265 },
            new ChartModel { X = "United States", Y = 3215 },
        };
    }
}
