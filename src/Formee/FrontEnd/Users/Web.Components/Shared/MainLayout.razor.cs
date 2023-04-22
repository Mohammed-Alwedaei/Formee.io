using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace Clients.Web.Components.Shared;

public partial class MainLayout : IDisposable
{
    [Inject]
    private AppStateService AppState { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject] 
    private ILogger<MainLayout> Logger { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState> authenticationState { get; set; }

    private Timer _timer;
    private DateTime _currentTime;
    private bool _navigationSidebarToggle = false;
    private bool _userSidebarToggle = true;

    protected override async Task OnParametersSetAsync()
    {
        var authState = await authenticationState;

        foreach (var claim in authState.User.Claims)
        {
            Logger.LogInformation("{type}: {value}", claim.Type, claim.Value);
        }

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