using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Services;

namespace Clients.Web.Components;

public partial class MainLayout : IDisposable
{
    [Inject]
    public ContainersService ContainersService { get; set; }

    [Inject]
    public NotificationsService NotificationsService { get; set; }

    [Inject]
    public AppStateService AppStateService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private Timer _timer;
    private DateTime _currentTime;
    private bool _navigationSidebarToggle = false;
    private bool _userSidebarToggle = true;

    private List<ContainerDto> _containers { get; set; }

    private bool _isFetching;

    private string? _userAuthProviderId;

    private List<string> _errors;

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        _isFetching = true;
        _timer = new Timer(GetCurrentTime, null, 0, 1000);
    }

    /// <summary>
    /// Get the current time 
    /// </summary>
    /// <param name="_"></param>
    public void GetCurrentTime(object _)
    {
        _currentTime = DateTime.Now;
        InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// Dispose 
    /// </summary>
    public void Dispose()
    {
        NotificationsService.StateChanged -= StateHasChanged;
        ContainersService.OnChange -= StateHasChanged;
        _timer?.Dispose();
    }
}