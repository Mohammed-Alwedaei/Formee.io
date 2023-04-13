using Client.Web.Utilities.Dtos.Identity;
using Client.Web.Utilities.Services;

namespace Clients.Web.Components.Pages.Dashboard.Identity;

[Route(Routes.Identity)]
public partial class Index
{
    [Inject]
    public IdentityService IdentityService { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "userId")]
    public string UserId { get; set; }

    [CascadingParameter(Name = "User")]
    private UserDto user { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        
    }
}
