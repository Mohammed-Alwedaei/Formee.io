namespace Clients.Web.Components;

public partial class MainLayout : IDisposable
{
    private Timer _timer;
    private DateTime _currentTime;
    private bool _sidebarToggle = true;

    protected override void OnInitialized()
    {
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

    public void Dispose()
    {
        _timer?.Dispose();
    }
}