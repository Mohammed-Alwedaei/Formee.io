namespace Subscriptions.BusinessLogic.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UsersModel>> GetAllAsync()
    {
        return await _context.Users
            .ToListAsync();
    }

    public async Task<List<UsersModel>> GetAllSubscribedAsync()
    {
        return await _context.Users
            //.Where(u => u.SubscriptionId != 0)
            .ToListAsync();
    }

    public async Task<List<UsersModel>> GetAllInASubscriptionAsync
        (int subscriptionId)
    {
        return await _context.Users
           // .Where(u => u.SubscriptionId == subscriptionId)
            .ToListAsync();
    }

    public async Task<UsersModel> CreateAsync(UserDto user)
    {
        var createdUser = await _context.Users
            .AddAsync(user);

        await _context.SaveChangesAsync();

        return createdUser.Entity;
    }

    public async Task<UsersModel> DeleteAsync(Guid userId)
    {
        var isUser = await _context.Users
            .FirstOrDefaultAsync(u => u.GlobalUserId == userId);

        if(isUser is null) return new UsersModel();

        var deletedUser = _context.Users
            .Remove(isUser);

        await _context.SaveChangesAsync();

        return deletedUser.Entity;
    }
}
