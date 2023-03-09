using Analytics.Utilities.Dtos.PageHit;

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

    public async Task<List<PageHitEntity>> GetAllBySiteId(int siteId)
    {
        return await _db.PageHit
            .Where(h => h.SiteId == siteId)
            .ToListAsync();
    }

    public async Task<List<PageHitEntity>> GetAllByCountryNameAsync
        (int siteId, string country)
    {
        return await _db.PageHit
            .Where(h => h.SiteId == siteId && h.Country == country)
            .ToListAsync();
    }

    public async Task<List<PageHitEntity>> GetAllByDateAsync
        (int siteId, DateTime startDate, DateTime endDate)
    {
        return await _db.PageHit
            .Where(h => h.SiteId == siteId 
                        && h.CreatedDate >= startDate 
                        && h.CreatedDate <= endDate)
            .ToListAsync();
    }

    public async Task<CreatePageHitDto> CreateAsync(CreatePageHitDto hit)
    {
        var hitToCreate = _mapper.Map<PageHitEntity>(hit);

        var result = await _db.PageHit
            .AddAsync(hitToCreate);

        await _db.SaveChangesAsync();

        return _mapper.Map<CreatePageHitDto>(result.Entity);
    }
}
