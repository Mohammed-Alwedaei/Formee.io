﻿using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Authorization;

namespace Clients.Web.Components.Pages.Users.Dashboard.Identity;

[Route(Routes.Identity)]
[Authorize]
public partial class Index : IDisposable
{
    [Inject]
    public IdentityService IdentityService { get; set; }

    [Inject]
    public AppStateService AppState { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "user_id")]
    public string UserId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (AppState.Identity.User.Id == Guid.Empty || AppState.Identity.User.Id != Guid.Parse(UserId))
            NavigationManager.NavigateTo($"{Routes.Aggregate}?redirect_to={NavigationManager.Uri}");

        if (string.IsNullOrEmpty(UserId))
            NavigationManager.NavigateTo("/error/notfound");
    }

    private async Task AggregatePageData(Guid userId)
    {
        AppState.Identity.StateChanged += StateHasChanged;
        await IdentityService.GetByIdAsync(userId);
    }

    public void Dispose()
    {
        AppState.Identity.StateChanged -= StateHasChanged;
    }
}
