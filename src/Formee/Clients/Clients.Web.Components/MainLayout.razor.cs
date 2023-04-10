﻿using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Clients.Web.Components;

public partial class MainLayout : IDisposable
{
    [Inject]
    public ContainersService ContainersService { get; set; }
    
    [Inject]
    public NotificationsService NotificationsService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private Timer _timer;
    private DateTime _currentTime;
    private bool _navigationSidebarToggle = false;
    private bool _userSidebarToggle = true;

    private List<ContainerDto> _containers { get; set; }

    private bool _isFetching;

    protected override async Task OnParametersSetAsync()
    {
        _isFetching = true;

        var userId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        NotificationsService.OnChange += StateHasChanged;

        ContainersService.OnChange += StateHasChanged;

        await ContainersService.GetAllByUserIdAsync(userId);

        await NotificationsService.CreateClientConnection();

        await NotificationsService.GetAllByUserIdAsync(userId, 5);

        NotificationsService.GetConnectionStatus();

        _isFetching = false;
    }

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider
            .GetAuthenticationStateAsync();

        var user = authState.User;

        if (user.Identity is not null && user.Identity.IsAuthenticated)
        {

            NavigationManager.NavigateTo("/");
        }

        _timer = new Timer(GetCurrentTime, null, 0, 1000);
    }

    public void GetCurrentTime(object _)
    {
        _currentTime = DateTime.Now;
        InvokeAsync(StateHasChanged);
    }

    public void HandleNavigationSidebarToggle()
    {
        _navigationSidebarToggle = !_navigationSidebarToggle;
    }
    
    public void HandleUserSidebarToggle()
    {
        _userSidebarToggle = !_userSidebarToggle;
    }

    private void HandleNewContainerClick()
    {
        NavigationManager.NavigateTo("/containers/upsert?type=create");
    }

    public void Dispose()
    {
        NotificationsService.OnChange -= StateHasChanged;
        ContainersService.OnChange -= StateHasChanged;
        _timer?.Dispose();
    }
}