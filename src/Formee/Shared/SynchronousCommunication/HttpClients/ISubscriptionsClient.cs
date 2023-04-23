using SynchronousCommunication.Dtos;

namespace SynchronousCommunication.HttpClients;

public interface ISubscriptionsClient
{
    Task<UserSubscriptionDto> GetSubscriptionFeaturesAsync(Guid userId);

    Task<SubscriptionDto> GetDefaultSubscription();

    Task<UserSubscriptionDto?> CreateUserAndAssignSubscriptionAsync
        (Guid userId, string email);
}