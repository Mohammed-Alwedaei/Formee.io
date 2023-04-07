namespace Subscriptions.BusinessLogic.Repositories.IRepository;

public interface ISubscriptionFeatureRepository
{
    Task<IReadOnlyList<SubscriptionFeatureDto>> GetAllAsync();

    Task<SubscriptionFeatureDto> CreateAsync(SubscriptionFeatureDto feature);

    Task<SubscriptionFeatureDto> UpdateAsync(SubscriptionFeatureDto feature);

    Task<SubscriptionFeatureDto> DeleteAsync(int id);
}
