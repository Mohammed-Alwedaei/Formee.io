using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Components;
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
    private bool _sidebarToggle = true;

    private List<ContainerDto> _containers { get; set; }

    private bool _isFetching;

    private IReadOnlyList<NotificationDto> Notifications { get; set; } = new List<NotificationDto>();

    protected override async Task OnParametersSetAsync()
    {
        _isFetching = true;

        var userId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        _containers = await ContainersService.GetAllByUserIdAsync(userId);

        Notifications = await NotificationsService.GetAllByUserIdAsync(userId, 4); ;

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

    public void Toggle()
    {
        _sidebarToggle = !_sidebarToggle;
    }

    private void HandleNewContainerClick()
    {
        NavigationManager.NavigateTo("/containers/upsert?type=create");
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}