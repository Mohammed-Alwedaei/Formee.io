using Client.Web.Utilities.Constants;
using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Services;
using Markdig;
using Microsoft.AspNetCore.Components;

namespace Clients.Web.Components.Pages.Dashboard.Containers;

[Route(Routes.UpsertContainer)]
public partial class Upsert
{
    [Inject]
    public ContainersService ContainersService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "containerId")]
    public string ContainerId { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "type")]
    public string UpsertType { get; set; }

    private ContainerDto _container = new();

    private ContainerDto _containerPreview = new();

    private bool IsDeleteOperation { get; set; }

    private bool IsVisibleModal { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        // if true then create new container
        // if not true then update a container
        if (string.IsNullOrEmpty(ContainerId) && UpsertType == "create")
        {
            _container = new ContainerDto();
        }
        else if (!string.IsNullOrEmpty(ContainerId)
                && UpsertType == "update"
                || UpsertType == "delete")
        {
            _container = await ContainersService.GetByIdAsync(ContainerId);

            if (UpsertType == "delete")
            {
                IsDeleteOperation = true;
            }
        }
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
        bool response;

        if (UpsertType == "delete")
        {
            response = await ContainersService.DeleteByIdAsync(ContainerId);
        }
        else if (UpsertType == "update")
        {
            response = await ContainersService.UpdateAsync(_container);
        }
        else
        {
            response = await ContainersService
                .CreateAsync(_container);
        }


        if (response)
        {
            NavigationManager.NavigateTo("/containers");
        }
        else
        {
            NavigationManager.NavigateTo("/");
        }
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
