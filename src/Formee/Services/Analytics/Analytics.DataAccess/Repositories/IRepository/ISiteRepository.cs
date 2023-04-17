namespace Analytics.BusinessLogic.Repositories.IRepository;

public interface ISiteRepository
{
    Task<SiteEntity?> GetSiteByIdAsync(int id);

    Task<List<SiteEntity>> GetAllSitesByContainerIdAsync(string containerId);
    Task<List<SiteEntity>> GetAllSitesByUserIdAsync(Guid userId);

    Task<CreateSiteDto> CreateSiteAsync(CreateSiteDto site);

    Task<UpdateSiteDto> UpdateSiteAsync(UpdateSiteDto site);

    Task<DeleteSiteDto> DeleteSiteByIdAsync(int id);
}
