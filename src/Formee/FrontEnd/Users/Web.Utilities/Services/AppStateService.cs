using Client.Web.Utilities.StateManagement;

namespace Client.Web.Utilities.Services;

public class AppStateService
{
    public readonly IdentityState Identity = new ();
    
    public readonly ContainersState Containers  = new ();

    public readonly AnalyticsState Analytics = new ();
    
    public readonly HistoryState History = new ();

    public readonly LinksState Links = new();
    
    public readonly NotificationsState Notifications = new();

    public readonly SubscriptionsState Subscriptions = new();
}
