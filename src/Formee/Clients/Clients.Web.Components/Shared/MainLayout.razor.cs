using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Clients.Web.Components.Shared;

public partial class MainLayout : IDisposable
{
    [Inject]
    public ContainersService ContainersService { get; set; }
    
    [Inject]
    public NotificationsService NotificationsService { get; set; }
    
    [Inject]
    public IdentityService IdentityService { get; set; }

    [Inject]
    public AppStateService AppStateService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState> authenticationState { get; set; }

    private Timer _timer;
    private DateTime _currentTime;
    private bool _navigationSidebarToggle = false;
    private bool _userSidebarToggle = true;

    protected override async Task OnInitializedAsync()
    {
        await AggregateUserData();
        _timer = new Timer(GetCurrentTime, null, 0, 1000);
    }

    /// <summary>
    /// Get the current time 
    /// </summary>
    /// <param name="_"></param>
    public void GetCurrentTime(object _)
    {
        _currentTime = DateTime.Now;
    }

    public async Task AggregateUserData()
    {
        var authState = await authenticationState;

        if (authState.User.Identity.IsAuthenticated)
        {
            var authProviderId = authState.User
                .Claims
                .FirstOrDefault(c => c.Type == "sub")
                ?.Value;

            var user = await IdentityService.GetByAuthIdAsync(authProviderId);
            
            AppStateService.OnUserStateChange += async () =>
            {
                await InvokeAsync(StateHasChanged);
            };
            
            if (user.Id == Guid.Empty)
            {
                NavigationManager
                    .NavigateTo($"{Routes.IdentityUpsert}?type=create&auth_provider_id={authProviderId}");
            }
            else
            {
                AppStateService.SetUserState(user);

                NavigationManager.GetUriWithQueryParameter("user_id", user.Id);
            }
        }
        else
        {
            NavigationManager.NavigateTo("/authentication/login");
        }
    }

    /// <summary>
    /// Dispose 
    /// </summary>
    public void Dispose()
    {
        AppStateService.OnUserStateChange -= StateHasChanged;
        _timer?.Dispose();
    }
}