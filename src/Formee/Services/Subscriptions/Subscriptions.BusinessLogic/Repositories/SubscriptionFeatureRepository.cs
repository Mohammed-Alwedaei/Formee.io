using AutoMapper;
using Subscriptions.BusinessLogic.Dtos.Subscriptions;
using Subscriptions.BusinessLogic.Models.Subscriptions;

namespace Subscriptions.BusinessLogic.Repositories;

public class SubscriptionFeatureRepository : ISubscriptionFeatureRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SubscriptionFeatureRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SubscriptionFeatureDto> GetOneByIdAsync(int id)
    {
        var featureModelFromDb = await _context.SubscriptionFeatures
            .FirstOrDefaultAsync(s => s.Id == id 
                                      && s.IsDeleted == false);

        return _mapper.Map<SubscriptionFeatureDto>(featureModelFromDb);
    }

    public async Task<List<SubscriptionFeatureDto>> GetAllAsync()
    {
        var featuresModelFromDb = await _context.SubscriptionFeatures
            .Where(s => s.IsDeleted == false)
            .ToListAsync();

        return _mapper.Map<List<SubscriptionFeatureDto>>(featuresModelFromDb);
    }

    public async Task<SubscriptionFeatureDto> CreateAsync
        (SubscriptionFeatureDto featureDto)
    {
        var featureModel = _mapper.Map<SubscriptionFeatureModel>(featureDto);

        var createdFeature = await _context.SubscriptionFeatures
            .AddAsync(featureModel);

        await _context.SaveChangesAsync();

        return _mapper.Map<SubscriptionFeatureDto>(createdFeature.Entity);
    }

    public async Task<SubscriptionFeatureDto> UpdateAsync
        (SubscriptionFeatureDto featureDto)
    {
        var featureModel = _mapper.Map<SubscriptionFeatureModel>(featureDto);

        var updatedFeature = _context.SubscriptionFeatures
            .Update(featureModel);

        await _context.SaveChangesAsync();

        return _mapper.Map<SubscriptionFeatureDto>(updatedFeature.Entity);
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

        return _mapper.Map<SubscriptionFeatureDto>(featureToDelete);
    }
}
