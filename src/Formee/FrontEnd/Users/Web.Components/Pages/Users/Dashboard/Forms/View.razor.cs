using Client.Web.Utilities.Dtos.Forms;
using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Authorization;

namespace Clients.Web.Components.Pages.Users.Dashboard.Forms;

[Authorize]
[Route(Routes.ViewForm)]
public partial class View : IDisposable
{
    [Inject]
    public AppStateService AppState { get; set; }

    [Inject]
    public FormsService FormsService { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "user_id")]
    public string UserId { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "container_id")]
    public string ContainerId { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "site_id")]
    public int SiteId { get; set; }
    
    [Parameter]
    [SupplyParameterFromQuery(Name = "form_id")]
    public int FormId { get; set; }

    private FormDto _formDto = new();
    private string _upsertType;
    private bool _isVisibleUpsertDialog;

    protected override async Task OnParametersSetAsync()
    {
        AppState.Forms.StateChanged += async () =>
            await InvokeAsync(StateHasChanged);

        await FormsService.GetIdAsync(FormId);
        _formDto = AppState.Forms.Form;
    }

    private void HandleFormEditEvent()
    {
        _upsertType = "update";
        _isVisibleUpsertDialog = true;
    }

    private void HandleFormDeleteEvent()
    {
        _upsertType = "delete";
        _isVisibleUpsertDialog = true;
    }

    public void Dispose()
    {
        AppState.Forms.StateChanged -= StateHasChanged;
    }
}
