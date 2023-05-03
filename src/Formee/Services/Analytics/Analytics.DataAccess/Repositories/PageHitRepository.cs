using Analytics.Utilities.Dtos.PageHit;
using System.Linq.Expressions;

namespace Analytics.BusinessLogic.Repositories;

public class PageHitRepository : IPageHitRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    public PageHitRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<List<PageHitDto>> GetAllBySiteId(int siteId)
    {
        var hits = await _db.PageHits
            .Include(p => p.Category)
            .AsNoTracking()
            .Where(h => h.SiteId == siteId)
            .ToListAsync();

        return _mapper.Map<List<PageHitDto>>(hits);
    }

    public async Task<List<PageHitDto>> GetAllByCountryNameAsync
        (int siteId, string country)
    {
        var hits = await _db.PageHits
            .AsNoTracking()
            .Where(h => h.SiteId == siteId && h.Country == country)
            .ToListAsync();

        return _mapper.Map<List<PageHitDto>>(hits);
    }

    public async Task<List<PageHitDto>> GetAllByDateAsync
        (int siteId,
            DateTime startDate,
            DateTime endDate,
            params Expression<Func<PageHitEntity, object>>[] includes)
    {
        var query = _db.PageHits.AsNoTracking();

        query = includes.
            Aggregate(query,
                (current, includeProperty)
                    => current.Include(includeProperty));

        var hits = await query.Where(h => h.SiteId == siteId
                                          && h.Category.IsDeleted != true
                                          && h.CreatedDate >= startDate
                                          && h.CreatedDate <= endDate)
            .ToListAsync();

        return _mapper.Map<List<PageHitDto>>(hits);
    }

    public async Task<List<DateChartDto>> GetAllByDateAndFormatAsync
        (int siteId, DateTime startDate, DateTime endDate)
    {

        var hits = await _db.PageHits
            .AsNoTracking()
            .Where(h => h.SiteId == siteId 
                        && h.Category.IsDeleted != true 
                        && h.CreatedDate >= startDate 
                        && h.CreatedDate <= endDate)
            .ToListAsync();

        var linkHitsChartDto = new List<DateChartDto>();

        foreach (var hit in hits)
        {
            var isAvailableDate = linkHitsChartDto
                .FirstOrDefault(c => c.Date.Date == hit.CreatedDate.Date);
            if (isAvailableDate is not null)
            {
                isAvailableDate.Count++;
            }
            else
            {
                linkHitsChartDto.Add(new DateChartDto
                {
                    Date = hit.CreatedDate,
                    Count = 1
                });
            }
        }

        return linkHitsChartDto;
    }

    public async Task<PageHitDto> CreateAsync(CreatePageHitDto hit)
    {
        var hitToCreate = _mapper.Map<PageHitEntity>(hit);

        var result = await _db.PageHits
            .AddAsync(hitToCreate);

        await _db.SaveChangesAsync();

        return _mapper.Map<PageHitDto>(result.Entity);
    }
}
