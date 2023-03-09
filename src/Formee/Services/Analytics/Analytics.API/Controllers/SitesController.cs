using Analytics.Utilities.Dtos.Site;

namespace Analytics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SitesController : ControllerBase
{
    private readonly ILogger<SitesController> _logger;
    private readonly ISiteRepository _siteRepository;

    public SitesController(ILogger<SitesController> logger, 
        ISiteRepository siteRepository)
    {
        _logger = logger;
        _siteRepository = siteRepository;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSiteByIdAsync(int id)
    {
        _logger.LogInformation("GET: request at /api/sites/{id} at {datetime}",
            id, DateTime.Now);

        var result = await _siteRepository.GetSiteByIdAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("all/{containerId}")]
    public async Task<IActionResult> GetSiteByIdAsync(string containerId)
    {
        _logger.LogInformation("GET: request at /api/sites/{containerId} at {datetime}",
            containerId, DateTime.Now);

        var result = await _siteRepository
            .GetAllSitesByContainerIdAsync(containerId);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSiteByIdAsync(CreateSiteDto site)
    {
        _logger.LogInformation("POST: request at /api/sites at {datetime}",
            DateTime.Now);

        var result = await _siteRepository.CreateSiteAsync(site);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSiteByIdAsync(UpdateSiteDto site)
    {
        _logger.LogInformation("PUT: request at /api/sites at {datetime}",
            DateTime.Now);

        var result = await _siteRepository.UpdateSiteAsync(site);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSiteByIdAsync(int id)
    {
        _logger.LogInformation("DELETE: request at /api/sites/{id} at {datetime}",
            id, DateTime.Now);

        var result = await _siteRepository.DeleteSiteByIdAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
