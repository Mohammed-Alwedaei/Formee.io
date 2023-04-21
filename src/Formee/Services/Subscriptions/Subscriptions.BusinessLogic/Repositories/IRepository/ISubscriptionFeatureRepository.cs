using Subscriptions.BusinessLogic.Dtos.Subscriptions;

namespace Subscriptions.BusinessLogic.Repositories.IRepository;

public interface ISubscriptionFeatureRepository
{
    Task<SubscriptionFeatureDto?> GetOneByIdAsync(int id);
    
    Task<List<SubscriptionFeatureDto>> GetAllAsync();

    Task<SubscriptionFeatureDto> CreateAsync(SubscriptionFeatureDto feature);

    Task<SubscriptionFeatureDto> UpdateAsync(SubscriptionFeatureDto feature);

    Task<SubscriptionFeatureDto> DeleteAsync(int id);
}
