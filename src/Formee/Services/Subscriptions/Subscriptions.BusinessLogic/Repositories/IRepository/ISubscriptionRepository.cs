namespace Subscriptions.BusinessLogic.Repositories.IRepository;

public interface ISubscriptionRepository
{
    Task<SubscriptionDto> GetOneById(int id);

    Task<List<SubscriptionDto>> GetAllAsync();

    Task<List<SubscriptionDto>> GetAllByAdminIdAsync(int adminId);

    Task<SubscriptionDto> CreateAsync(SubscriptionDto subscription);

    Task<SubscriptionDto> UpdateAsync(SubscriptionDto subscription);

    Task<UsersModel> AddSubscriptionToUserAsync(int userId, int subscriptionId);

    Task<UsersModel> RemoveSubscriptionFromUserAsync(int userId);

    Task<SubscriptionDto> DeleteAsync(int subscriptionId);
}
