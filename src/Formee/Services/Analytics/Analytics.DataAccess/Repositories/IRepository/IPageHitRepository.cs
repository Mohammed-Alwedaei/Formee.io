using Analytics.Utilities.Dtos.PageHit;
using System.Linq.Expressions;

namespace Analytics.BusinessLogic.Repositories.IRepository;

public interface IPageHitRepository
{
    Task<List<PageHitDto>> GetAllBySiteId(int siteId);

    Task<List<PageHitDto>> GetAllByCountryNameAsync
        (int siteId, string country);

    Task<List<PageHitDto>> GetAllByDateAsync
        (int siteId, 
            DateTime startDate,
            DateTime endDate,
            params Expression<Func<PageHitEntity, object>>[] includes);

    Task<List<DateChartDto>> GetAllByDateAndFormatAsync
        (int siteId, DateTime startDate, DateTime endDate);

    Task<PageHitDto> CreateAsync(CreatePageHitDto hit);
}
