using Client.Web.Utilities.Dtos.Links;
using Client.Web.Utilities.Services;

namespace Clients.Web.Components.Pages.Users.Dashboard.Links;

[Route(Routes.LinksView)]
public partial class View : IDisposable
{
    [Inject]
    public AppStateService AppState { get; set; }

    [Inject]
    public LinksService LinksService { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "user_id")]
    public string UserId { get; set; }
    
    [Parameter]
    [SupplyParameterFromQuery(Name = "container_id")]
    public string ContainerId { get; set; }
    
    [Parameter]
    [SupplyParameterFromQuery(Name = "link_id")]
    public int LinkId { get; set; }

    private LinkDto _linkDto = new();

    private bool _isVisibleDeleteLinkDialog;

    private readonly string[] _palettes = { "#9e8ddf" };

    protected override async Task OnParametersSetAsync()
    {
        AppState.Links.StateChanged += async() =>
        {
            
            await InvokeAsync(StateHasChanged);
        };

        await LinksService.GetLinkById(LinkId);
        await LinksService.GetLinkHitsById(LinkId);

        _linkDto = AppState.Links.Link;
    }

    private void RevealDeleteLinkDialog() => _isVisibleDeleteLinkDialog = true;

    private async Task HandleDeleteLinkApprove()
    {
        _linkDto.LinkDetails.Name = _linkDto.Name;

        await LinksService.DeleteAsync(_linkDto.Id);
    }

    private async Task HandleLinkHitsRefresh() => 
        await LinksService.GetLinkHitsById(LinkId);

    private void HandleDeleteLinkReject() => _isVisibleDeleteLinkDialog = false;
    
    public void Dispose()
    {
        AppState.Links.StateChanged -= StateHasChanged;
    }
}
