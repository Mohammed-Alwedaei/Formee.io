using Client.Web.Utilities.Services;

namespace Clients.Web.Components.Shared;

public partial class MainLayout : IDisposable
{
    [Inject]
    private AppStateService AppState { get; set; }

    private Timer _timer;
    private DateTime _currentTime;
    private bool _navigationSidebarToggle = false;
    private bool _userSidebarToggle = true;

    protected override async Task OnParametersSetAsync()
    {
        _timer = new Timer(GetCurrentTime, null, 0, 1000);
    }

    /// <summary>
    /// Get the current time 
    /// </summary>
    /// <param name="_"></param>
    private void GetCurrentTime(object _)
    {
        _currentTime = DateTime.Now;
    }

    /// <summary>
    /// Dispose 
    /// </summary>
    public void Dispose()
    {
        _timer?.Dispose();
    }
}