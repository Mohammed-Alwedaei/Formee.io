using Analytics.Utilities.Dtos.PageHit;

namespace Analytics.BusinessLogic.Repositories.IRepository;

public interface IPageHitRepository
{
    Task<List<PageHitEntity>> GetAllBySiteId(int siteId);

    Task<List<PageHitEntity>> GetAllByCountryNameAsync
        (int siteId, string country);

    Task<List<PageHitEntity>> GetAllByDateAsync
        (int siteId, DateTime startDate, DateTime endDate);

    Task<CreatePageHitDto> CreateAsync(CreatePageHitDto hit);
}
