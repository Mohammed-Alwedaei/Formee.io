using Subscriptions.BusinessLogic.Dtos.Subscriptions;
using Subscriptions.BusinessLogic.Models.Users;

namespace Subscriptions.BusinessLogic.Repositories.IRepository;

public interface ISubscriptionRepository
{
    Task<SubscriptionDto> GetOneById(int id);
    
    Task<SubscriptionDto> GetDefaultAsync();

    Task<List<SubscriptionDto>> GetAllAsync();

    Task<List<SubscriptionDto>> GetAllByAdminEmailAsync(string adminEmail);

    Task<SubscriptionDto> CreateAsync(SubscriptionDto subscription);

    Task<SubscriptionDto> UpdateAsync(SubscriptionDto subscription);

    Task<UserSubscriptionModel> UpsertSubscriptionToUserAsync
        (UpdateUserSubscriptionDto newUserSubscription);

    Task<UserSubscriptionModel> RemoveSubscriptionFromUserAsync(int userId);

    Task<SubscriptionDto> DeleteAsync(int subscriptionId);
}
