namespace Subscriptions.BusinessLogic.Repositories.IRepository;

public interface IUserRepository
{
    Task<List<UsersModel>> GetAllAsync();

    Task<List<UsersModel>> GetAllSubscribedAsync();
    
    Task<UserSubscriptionDto> GetSubscriptionByIdAsync(Guid userId);

    Task<List<UsersModel>> GetAllInASubscriptionAsync(int subscriptionId);

    Task<UsersModel> CreateAsync(UserDto user);

    Task<UsersModel> DeleteAsync(Guid userId);
}
