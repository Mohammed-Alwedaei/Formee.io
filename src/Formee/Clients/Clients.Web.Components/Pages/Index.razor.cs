using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Services;

namespace Clients.Web.Components.Pages;
public partial class Index
{
    [Inject]
    public ContainersService ContainersService { get; set; }

    private List<ContainerDto> _containers = new ();

    protected override async Task OnParametersSetAsync()
    {
        var userId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        _containers = await ContainersService.GetAllByUserIdAsync(userId);
    }
}
