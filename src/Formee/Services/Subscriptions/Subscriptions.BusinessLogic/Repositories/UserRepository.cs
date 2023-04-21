using AutoMapper;
using Subscriptions.BusinessLogic.Dtos.Users;
using Subscriptions.BusinessLogic.Models.Users;

namespace Subscriptions.BusinessLogic.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var usersFromDb = await _context.Users
            .ToListAsync();

        return _mapper.Map<List<UserDto>>(usersFromDb);
    }

    public async Task<List<UserDto>> GetAllSubscribedAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<UserSubscriptionDto> GetSubscriptionByIdAsync(Guid userId)
    {
        var userFromDb = await _context.Users.FirstOrDefaultAsync(u => u.GlobalUserId == userId);
        
        if(userFromDb.Id is 0) return new UserSubscriptionDto();
        
        var subscriptionFromDb = await _context.UserSubscriptions
            .Include(u => u.User)
            .Include(u => u.Subscription)
            .ThenInclude(s => s.SubscriptionFeatures)
            .FirstOrDefaultAsync(s => s.UserId == userFromDb.Id);

        return _mapper.Map<UserSubscriptionDto>(subscriptionFromDb) 
               ?? new UserSubscriptionDto();
    }

    public async Task<List<UserDto>> GetAllInASubscriptionAsync(int subscriptionId)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> CreateAsync(UserDto user)
    {
        var userModel = _mapper.Map<UserModel>(user);

        var createdUser = await _context.Users
            .AddAsync(userModel);

        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(createdUser);
    }

    public async Task<UserDto> DeleteAsync(Guid userId)
    {
        var isUser = await _context.Users
            .FirstOrDefaultAsync(u => u.GlobalUserId == userId);

        if(isUser is null) return new UserDto();

        var deletedUser = _context.Users
            .Remove(isUser);

        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(deletedUser.Entity);
    }
}
