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

    public async Task<UserSubscriptionModel> UpsertSubscriptionToUserAsync
        (int userId, int subscriptionId)
    {
        var userSubscriptionFromDb = await _context.UserSubscriptions
            .FirstOrDefaultAsync(u => u.Id == userId);

        //If true add a new record 
        if (userSubscriptionFromDb is null)
        {

            var userSubscriptionToCreate = new UserSubscriptionModel
            {
                UserId = userId,
                SubscriptionId = subscriptionId
            };

            await _context.UserSubscriptions.AddAsync(userSubscriptionToCreate);
            await _context.SaveChangesAsync();

            return userSubscriptionToCreate;

        } 
        //if false modify the record
        userSubscriptionFromDb.SubscriptionId = subscriptionId;

        _context.UserSubscriptions.Update(userSubscriptionFromDb);

        await _context.SaveChangesAsync();

        return userSubscriptionFromDb;
    }

    public async Task<UserSubscriptionModel> RemoveSubscriptionFromUserAsync(int userId)
    {
        var userSubscriptionFromDb = await _context.UserSubscriptions
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (userSubscriptionFromDb is null)
        {
            return new UserSubscriptionModel();
        }

        _context.UserSubscriptions.Remove(userSubscriptionFromDb);

        await _context.SaveChangesAsync();

        return userSubscriptionFromDb;
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
