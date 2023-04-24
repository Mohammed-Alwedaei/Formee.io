namespace Analytics.BusinessLogic.Repositories;

public class SiteRepository : ISiteRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public SiteRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<SiteEntity?> GetSiteByIdAsync(int id)
    {
        return await _db.Sites
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id 
                                      && s.IsDeleted == false);
    }

    public async Task<List<SiteEntity>> GetAllSitesByContainerIdAsync(string containerId)
    {
        return await _db.Sites
            .AsNoTracking()
            .Where(s => s.ContainerId == containerId 
                        && s.IsDeleted == false)
            .ToListAsync();
    }
    
    public async Task<List<SiteEntity>> GetAllSitesByUserIdAsync(Guid userId)
    {
        return await _db.Sites
            .AsNoTracking()
            .Where(s => s.UserId == userId 
                        && s.IsDeleted == false)
            .ToListAsync();
    }

    public async Task<CreateSiteDto> CreateSiteAsync(CreateSiteDto site)
    {
        var siteToCreate = _mapper.Map<SiteEntity>(site);

        var result = await _db.Sites
            .AddAsync(siteToCreate);

        await _db.SaveChangesAsync();

        if (result is null)
        {
            return new CreateSiteDto();
        }

        return _mapper.Map<CreateSiteDto>(siteToCreate);
    }

    public async Task<UpdateSiteDto> UpdateSiteAsync(UpdateSiteDto site)
    {
        var siteToUpdate = _mapper.Map<SiteEntity>(site);

        var result = _db.Sites.Update(siteToUpdate);

        await _db.SaveChangesAsync();

        if (result is null)
        {
            return new UpdateSiteDto();
        }

        return _mapper.Map<UpdateSiteDto>(siteToUpdate);
    }

    public async Task<DeleteSiteDto> DeleteSiteByIdAsync(int id)
    {
        var siteToDelete = await GetSiteByIdAsync(id);

        if (siteToDelete is null)
        {
            return new DeleteSiteDto();
        }

        siteToDelete.IsDeleted = true;
        siteToDelete.DeletedDate = DateTime.UtcNow;

        _db.Sites.Update(siteToDelete);

        await _db.SaveChangesAsync();

        return _mapper.Map<DeleteSiteDto>(siteToDelete);
    }
}
