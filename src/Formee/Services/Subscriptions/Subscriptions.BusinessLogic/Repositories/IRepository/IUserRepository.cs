using Subscriptions.BusinessLogic.Dtos.Users;

namespace Subscriptions.BusinessLogic.Repositories.IRepository;

public interface IUserRepository
{
    Task<List<UserDto>> GetAllAsync();

    Task<UserSubscriptionDto> GetSubscriptionByIdAsync(Guid userId);

    Task<UserDto> CreateAsync(UserDto user);

    Task<UserDto> DeleteAsync(Guid userId);
}
