﻿using Client.Web.Utilities.Services;
using Syncfusion.Blazor.Buttons;
using Syncfusion.Blazor.SplitButtons;

namespace Clients.Web.Components.Pages.Dashboard.Containers;

[Route(Routes.Containers)]
public partial class Index : IDisposable
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public ContainersService ContainersService { get; set; }

    [Inject]
    public AnalyticsService AnalyticsService { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "user_id")]
    public string UserId { get; set; }

    protected override void OnParametersSet()
    {
        ContainersService.OnChange += async () =>
        {
            await ContainersService.GetAllByUserIdAsync(Guid.Parse(UserId));
            await InvokeAsync(StateHasChanged);
        };
    }

    /// <summary>
    /// Redirect the user to create new container page
    /// </summary>
    public void NavigateToNewContainer()
    {
        NavigationManager.NavigateTo($"{Routes.UpsertContainer}?type=create");
    }

    /// <summary>
    /// Redirect the user to view the container details
    /// </summary>
    /// <param name="containerId"></param>
    public void HandleContainerClick(string? containerId)
    {
        NavigationManager.NavigateTo($"{Routes.ViewContainer}?user_id={UserId}&container_id={containerId}");
    }

    /// <summary>
    /// Detect the event sent by the user and redirect to the corresponding upsert page
    /// </summary>
    /// <param name="args"></param>
    private void ItemSelected(MenuEventArgs args)
    {
        NavigationManager.NavigateTo(args.Item.Text == "Edit"
            ? $"{Routes.UpsertContainer}?user_id={UserId}&upsert_type=update&container_id={args.Item.Id}"
            : $"{Routes.UpsertContainer}?user_id={UserId}&upsert_type=delete&container_id={args.Item.Id}");
    }

    /// <summary>
    /// Redirect the user to the corresponding site when clicking on a site in the 
    /// </summary>
    /// <param name="args"></param>
    private void HandleSiteSelect(ChipEventArgs args)
    {
        var site = AnalyticsService.Sites
            .FirstOrDefault(s => s.Name == args.Text);

        NavigationManager.NavigateTo($"{Routes.Sites}?user_id={UserId}&container_id={site.ContainerId}");
    }

    /// <summary>
    /// Dispose the free and unused resources
    /// </summary>
    public void Dispose() => ContainersService.OnChange -= StateHasChanged;
}
