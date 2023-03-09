using Analytics.Utilities.Dtos.PageHit;

namespace Analytics.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HitsController : ControllerBase
{
    private readonly IPageHitRepository _hitRepository;
    private readonly ILogger<HitsController> _logger;   

    public HitsController(IPageHitRepository hitRepository, 
        ILogger<HitsController> logger)
    {
        _hitRepository = hitRepository;
        _logger = logger;
    }

    [HttpGet("all/{siteId:int}/{country}")]
    public async Task<IActionResult> GetAllHitsByCountryName(int siteId,string country)
    {
        _logger.LogInformation("GET: request at /api/hits/{siteId}/{country} at {datetime}",
            siteId, country, DateTime.Now);

        var result = await _hitRepository
            .GetAllByCountryNameAsync(siteId, country);

        if (result.Count is 0)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("all/{siteId:int}/{startDate}/{endDate}")]
    public async Task<IActionResult> GetAllInTimePeriod
        (int siteId, DateTime startDate, DateTime endDate)
    {
        _logger.LogInformation("GET: request at /api/hits/all/{siteId}/{startDate}/{endDate} at {datetime}",
            siteId, startDate, endDate, DateTime.Now);

        var result = await _hitRepository
            .GetAllByDateAsync(siteId, startDate, endDate);

        if (result.Count is 0)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("all/{siteId:int}")]
    public async Task<IActionResult> GetAllBySiteId(int siteId)
    {
        _logger.LogInformation("GET: request at /api/hits/{siteId} at {datetime}",
            siteId, DateTime.Now);

        var result = await _hitRepository.GetAllBySiteId(siteId);

        if (result.Count is 0)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHit(CreatePageHitDto hit)
    {
        _logger.LogInformation("POST: request at /api/hits at {datetime}", 
            DateTime.Now);

        var result = await _hitRepository.CreateAsync(hit);

        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}
