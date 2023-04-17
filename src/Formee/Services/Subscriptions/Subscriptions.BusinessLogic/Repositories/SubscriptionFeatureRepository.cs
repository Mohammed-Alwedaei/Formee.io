namespace Subscriptions.BusinessLogic.Repositories;

public class SubscriptionFeatureRepository : ISubscriptionFeatureRepository
{
    private readonly ApplicationDbContext _context;

    public SubscriptionFeatureRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SubscriptionFeatureDto> GetOneByIdAsync(int id)
    {
        return await _context.SubscriptionFeatures
            .FirstOrDefaultAsync(s => s.Id == id && s.IsDeleted == false);
    }

    public async Task<IReadOnlyList<SubscriptionFeatureDto>> GetAllAsync()
    {
        return await _context.SubscriptionFeatures
            .Where(s => s.IsDeleted == false)
            .Select(s => (SubscriptionFeatureDto)s)
            .ToListAsync();
    }

    public async Task<SubscriptionFeatureDto> CreateAsync
        (SubscriptionFeatureDto feature)
    {
        var createdFeature = await _context.SubscriptionFeatures
            .AddAsync(feature);

        await _context.SaveChangesAsync();

        return createdFeature.Entity;
    }

    public async Task<SubscriptionFeatureDto> UpdateAsync
        (SubscriptionFeatureDto feature)
    {
        var updatedFeature = _context.SubscriptionFeatures
            .Update(feature);

        await _context.SaveChangesAsync();

        return updatedFeature.Entity;
    }

    public async Task<SubscriptionFeatureDto> DeleteAsync
        (int id)
    {
        var featureToDelete = await _context.SubscriptionFeatures
            .FirstOrDefaultAsync(s => s.Id == id 
                                      && s.IsDeleted == false);

        if (featureToDelete is null)
        {
            return new SubscriptionFeatureDto();
        }

        featureToDelete.IsDeleted = true;
        featureToDelete.DeletedDate = DateTime.Now;

        _context.SubscriptionFeatures.Update(featureToDelete);

        await _context.SaveChangesAsync();

        return featureToDelete;
    }
}
