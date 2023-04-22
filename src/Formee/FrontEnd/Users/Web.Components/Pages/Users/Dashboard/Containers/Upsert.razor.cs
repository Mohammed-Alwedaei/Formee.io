using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Services;
using Markdig;
using Microsoft.AspNetCore.Authorization;

namespace Clients.Web.Components.Pages.Users.Dashboard.Containers;

[Route(Routes.UpsertContainer)]
[Authorize(Policy = "users")]
public partial class Upsert
{
    [Inject]
    public ContainersService ContainersService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public AppStateService AppState { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "user_id")]
    public string UserId { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "container_id")]
    public string ContainerId { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "upsert_type")]
    public string UpsertType { get; set; }

    private ContainerDto? _container = new();

    private ContainerDto _containerPreview = new();

    private bool IsDeleteOperation { get; set; }

    private bool IsVisibleModal { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        // if true then create new container
        // if not true then update a container
        if (string.IsNullOrEmpty(ContainerId) && UpsertType == "create")
            _container = new ContainerDto();

        else if (!string.IsNullOrEmpty(ContainerId) && UpsertType == "update" || UpsertType == "delete")
        {
            await ContainersService.GetByIdAsync(ContainerId);
            _container = AppState.Containers.Container;

            if (UpsertType == "delete")
                IsDeleteOperation = true;
        }

        _container.UserId = Guid.Parse(UserId);
    }

    ///
    private async Task HandleValidFormSubmit()
    {
        //Display an accept model
        //when approved delete the container and return back to the containers index page
        //otherwise fallback and return to containers page
        IsVisibleModal = true;
    }

    /// <summary>
    /// On operation approve send api request to containers api
    /// </summary>
    /// <returns></returns>
    private async Task HandleActionApprove()
    {
        var response = UpsertType switch
        {
            "delete" => await ContainersService.DeleteByIdAsync(ContainerId),
            "update" => await ContainersService.UpdateAsync(_container),
            _ => await ContainersService.CreateAsync(_container)
        };

        var redirectUrl = !string.IsNullOrEmpty(response.Id)
            ? $"{Routes.Containers}?user_id={UserId}&container_id={response.Id}"
            : "/";

        NavigationManager.NavigateTo(redirectUrl);
    }

    /// <summary>
    /// Hide modal on operation cancel
    /// </summary>
    private void HandleActionReject()
    {
        IsVisibleModal = false;
    }

    /// <summary>
    /// Handle live preview display
    /// </summary>
    private void HandleContainerDescriptionChange()
    {
        if (_container.Description != null)
        {
            _containerPreview.Description = Markdown.ToHtml(_container.Description);
        }
    }
}
