using Subscriptions.BusinessLogic.Dtos.Users;
using Subscriptions.BusinessLogic.Models.Users;

namespace Subscriptions.BusinessLogic.Repositories.IRepository;

public interface IUserRepository
{
    Task<List<UserDto>> GetAllAsync();

    Task<List<UserDto>> GetAllSubscribedAsync();
    
    Task<UserSubscriptionDto> GetSubscriptionByIdAsync(Guid userId);

    Task<List<UserDto>> GetAllInASubscriptionAsync(int subscriptionId);

    Task<UserDto> CreateAsync(UserDto user);

    Task<UserDto> DeleteAsync(Guid userId);
}
