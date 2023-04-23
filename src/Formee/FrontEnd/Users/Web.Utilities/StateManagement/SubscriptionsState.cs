using Client.Web.Utilities.Dtos.Subscriptions;

namespace Client.Web.Utilities.StateManagement;

public class SubscriptionsState
{
    public SubscriptionDto Subscription;

    public bool IsFetching;

    public void SetSubscriptionState(SubscriptionDto subscription)
    {
        Subscription = subscription;
    }
}
