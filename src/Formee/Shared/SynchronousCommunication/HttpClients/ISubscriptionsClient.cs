using SynchronousCommunication.Dtos;

namespace SynchronousCommunication.HttpClients;

public interface ISubscriptionsClient
{
    Task<UserSubscriptionDto> GetSubscriptionFeaturesAsync(Guid userId);
}