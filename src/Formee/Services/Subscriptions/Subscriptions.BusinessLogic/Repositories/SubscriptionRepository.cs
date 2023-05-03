using AutoMapper;
using Subscriptions.BusinessLogic.Dtos.Subscriptions;
using Subscriptions.BusinessLogic.Models.Subscriptions;
using Subscriptions.BusinessLogic.Models.Users;

namespace Subscriptions.BusinessLogic.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SubscriptionRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SubscriptionDto> GetOneById(int id)
    {
        var subscriptionFromDb = await _context.Subscriptions
            .AsNoTracking()
            .Include(s => s.SubscriptionFeatures)
            .FirstOrDefaultAsync(s => s.Id == id
                                      && s.IsDeleted != true);
        
        if (subscriptionFromDb is null) return new SubscriptionDto();

        return _mapper.Map<SubscriptionDto>(subscriptionFromDb)
               ?? new SubscriptionDto();
    }

    public async Task<SubscriptionDto> GetDefaultAsync()
    {
        var subscriptionFromDb = await _context.Subscriptions
            .AsNoTracking()
            .Include(s => s.SubscriptionFeatures)
            .FirstOrDefaultAsync(s => s.IsDefault == true
                                      && s.IsDeleted != true);

        return _mapper.Map<SubscriptionDto>(subscriptionFromDb) 
               ?? new SubscriptionDto();
    }

    public async Task<List<SubscriptionDto>> GetAllAsync()
    {
        var listFromDb = await _context.Subscriptions
            .AsNoTracking()
            .Include(s => s.SubscriptionFeatures)
            .Where(s => s.IsDeleted != true)
            .ToListAsync();

        return _mapper.Map<List<SubscriptionDto>>(listFromDb.ToList());
    }

    public async Task<List<SubscriptionDto>> GetAllByAdminEmailAsync(string adminEmail)
    {
        var subscriptionsFromDb = await _context.Subscriptions
            .AsNoTracking()
            .Where(s => s.AdminEmail == adminEmail)
            .ToListAsync();

        return _mapper.Map<List<SubscriptionDto>>
            (subscriptionsFromDb.ToList());
    }

    public async Task<SubscriptionDto> CreateAsync(SubscriptionDto subscriptionDto)
    {
        var subscriptionModel = _mapper.Map<SubscriptionModel>(subscriptionDto);

        var createdSubscription = await _context.Subscriptions
            .AddAsync(subscriptionModel);

        await _context.SaveChangesAsync();

        return _mapper.Map<SubscriptionDto>(createdSubscription.Entity);
    }

    public async Task<SubscriptionDto> UpdateAsync
        (SubscriptionDto subscriptionDto)
    {
        var subscriptionModel = _mapper.Map<SubscriptionModel>(subscriptionDto);

        var updatedSubscription = _context.Subscriptions
            .Update(subscriptionModel);

        await _context.SaveChangesAsync();

        return _mapper.Map<SubscriptionDto>(updatedSubscription.Entity)
                       ?? new SubscriptionDto();
    }

    public async Task<UserSubscriptionModel> UpsertSubscriptionToUserAsync
        (UpdateUserSubscriptionDto newUserSubscription)
    {
        var userId = newUserSubscription.UserId;
        var subscriptionId = newUserSubscription.SubscriptionId;

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
            .FirstOrDefaultAsync(u => u.UserId == userId);

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

        return _mapper.Map<SubscriptionDto>(subscriptionFromDb);
    }
}
