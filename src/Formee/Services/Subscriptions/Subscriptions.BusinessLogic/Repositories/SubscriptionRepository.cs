namespace Subscriptions.BusinessLogic.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _context;

    public SubscriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SubscriptionDto> GetOneById(int id)
    {
        var subscriptionFromDb = await _context.Subscriptions
            .FirstOrDefaultAsync(s => s.Id == id
                                      && s.IsDeleted != true);
        if (subscriptionFromDb is null) return new SubscriptionDto();

        return subscriptionFromDb;
    }

    public async Task<List<SubscriptionDto>> GetAllAsync()
    {
        var listFromDb = await _context.Subscriptions.ToListAsync();

        return listFromDb
            .Select(a => (SubscriptionDto)a)
            .ToList();
    }

    public async Task<List<SubscriptionDto>> GetAllByAdminIdAsync(int adminId)
    {
        var listFromDb = await _context.Subscriptions
            .Where(s => s.AdminId == adminId)
            .ToListAsync();

        return listFromDb
            .Select(a => (SubscriptionDto)a)
            .ToList();
    }

    public async Task<SubscriptionDto> CreateAsync
        (SubscriptionDto subscription)
    {
        var createdSubscription = await _context.Subscriptions
            .AddAsync(subscription);

        await _context.SaveChangesAsync();

        return createdSubscription.Entity;
    }

    public async Task<SubscriptionDto> UpdateAsync
        (SubscriptionDto subscription)
    {
        var updatedSubscription = _context.Subscriptions
            .Update(subscription);

        await _context.SaveChangesAsync();

        return updatedSubscription.Entity;
    }

    public async Task<UsersModel> AddSubscriptionToUserAsync
        (int userId, int subscriptionId)
    {
        var userFromDb = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (userFromDb is null)
        {
            return new UsersModel();
        }

        userFromDb.SubscriptionId = subscriptionId;

        _context.Users.Update(userFromDb);

        await _context.SaveChangesAsync();

        return userFromDb;
    }

    public async Task<UsersModel> RemoveSubscriptionFromUserAsync(int userId)
    {
        var userFromDb = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (userFromDb is null)
        {
            return new UsersModel();
        }

        userFromDb.SubscriptionId = null;

        _context.Users.Update(userFromDb);

        await _context.SaveChangesAsync();

        return userFromDb;
    }

    public async Task<SubscriptionDto> DeleteAsync
        (int subscriptionId)
    {
        var subscriptionFromDb = await _context.Subscriptions
            .FirstOrDefaultAsync(s => s.Id == subscriptionId);

        if (subscriptionFromDb is null)
        {
            return new SubscriptionDto();
        }

        subscriptionFromDb.IsDeleted = true;

        _context.Subscriptions.Update(subscriptionFromDb);

        await _context.SaveChangesAsync();

        return subscriptionFromDb;
    }
}
