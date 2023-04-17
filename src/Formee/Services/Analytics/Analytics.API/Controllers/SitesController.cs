using Analytics.Utilities.Dtos.Site;
using SynchronousCommunication.HttpClients;

namespace Analytics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SitesController : ControllerBase
{
    private readonly ILogger<SitesController> _logger;
    private readonly ISubscriptionsClient _subscriptionsClient;
    private readonly ISiteRepository _siteRepository;

    public SitesController(ILogger<SitesController> logger, 
        ISiteRepository siteRepository, 
        ISubscriptionsClient subscriptionsClient)
    {
        _logger = logger;
        _siteRepository = siteRepository;
        _subscriptionsClient = subscriptionsClient;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSiteByIdAsync(int id)
    {
        _logger.LogInformation("GET: request at /api/sites/{id} at {datetime}",
            id, DateTime.Now);

        var result = await _siteRepository.GetSiteByIdAsync(id);

        if (result is null) return NotFound();
        
        return Ok(result);
    }

    [HttpGet("all/{containerId}")]
    public async Task<IActionResult> GetSiteByIdAsync(string containerId)
    {
        _logger.LogInformation("GET: request at /api/sites/{containerId} at {datetime}",
            containerId, DateTime.Now);

        var result = await _siteRepository
            .GetAllSitesByContainerIdAsync(containerId);

        if (result is null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSiteByIdAsync(CreateSiteDto site)
    {
        _logger.LogInformation("POST: request at /api/sites at {datetime}",
            DateTime.Now);

        var userSubscription = await _subscriptionsClient.GetSubscriptionFeaturesAsync(site.UserId);

        var userSites = await _siteRepository.GetAllSitesByUserIdAsync(site.UserId);

        var numberOfRemainingSites = userSubscription.Subscription.SubscriptionFeatures.NumberOfSites - userSites.Count;

        if (numberOfRemainingSites is 0)
            return Forbid();

        site.Domain = HttpContext.Request.Host.Host;

        var result = await _siteRepository.CreateSiteAsync(site);

        if (result is null) return NotFound();

        return Created("/", result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSiteByIdAsync(UpdateSiteDto site)
    {
        _logger.LogInformation("PUT: request at /api/sites at {datetime}",
            DateTime.Now);

        var result = await _siteRepository.UpdateSiteAsync(site);

        if (result is null) return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSiteByIdAsync(int id)
    {
        _logger.LogInformation("DELETE: request at /api/sites/{id} at {datetime}",
            id, DateTime.Now);

        var result = await _siteRepository.DeleteSiteByIdAsync(id);

        if (result is null) return NotFound();

        return Ok(result);
    }
}
