﻿using Analytics.Utilities.Dtos.PageHit;
using Analytics.Utilities.Dtos.Session;
using Microsoft.AspNetCore.Authorization;

namespace Analytics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HitsController : ControllerBase
{
    private readonly IPageHitRepository _hitRepository;
    private readonly ISiteRepository _siteRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly ILogger<HitsController> _logger;

    public HitsController(IPageHitRepository hitRepository,
        ILogger<HitsController> logger,
        ISiteRepository siteRepository,
        ISessionRepository sessionRepository)
    {
        _hitRepository = hitRepository;
        _logger = logger;
        _siteRepository = siteRepository;
        _sessionRepository = sessionRepository;
    }

    [HttpGet("all/{siteId:int}/{country}")]
    public async Task<IActionResult> GetAllHitsByCountryName(int siteId, string country)
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

    [HttpGet("all/{siteId:int}/{startDate:DateTime}/{endDate:DateTime}")]
    public async Task<IActionResult> GetAllInTimePeriod
        (int siteId,
            DateTime startDate,
            DateTime endDate,
            [FromQuery] string filter = "sites")
    {
        _logger.LogInformation("GET: request at /api/hits/all/{siteId}/{startDate}/{endDate} at {datetime}",
            siteId, startDate, endDate, DateTime.Now);

        if (string.Equals(filter, "sites"))
        {
            var result = await _hitRepository
                .GetAllByDateAsync(siteId, startDate, endDate, x => x.Site);

            return result is { Count: > 0 } ? Ok(result) : NotFound();
        }

        if (string.Equals(filter, "categories"))
        {
            var result = await _hitRepository
                .GetAllByDateAsync(siteId, startDate, endDate, x => x.Category);

            return result is { Count: > 0 } ? Ok(result) : NotFound();
        }

        if (string.Equals(filter, "all"))
        {
            var result = await _hitRepository
                .GetAllByDateAsync(siteId,
                    startDate,
                    endDate,
                    x => x.Site,
                    x => x.Category);

            return result is { Count: > 0 } ? Ok(result) : NotFound();
        }

        if (string.Equals(filter, "none"))
        {
            var result = await _hitRepository
                .GetAllByDateAsync(siteId,
                    startDate,
                    endDate);

            return result is { Count: > 0 } ? Ok(result) : NotFound();
        }

        if (string.Equals(filter, "modeled"))
        {
            var formatted = await _hitRepository
                .GetAllByDateAndFormatAsync(siteId,
                    startDate,
                    endDate);

            return formatted is { Count: > 0 } ? Ok(formatted) : NotFound();
        }

        return BadRequest();
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
        _logger.LogInformation("POST: request at /api/hits at {datetime}", DateTime.Now);

        var hasActiveSession = await _sessionRepository
            .GetAsync(hit.DeviceId);

        //If the user doesn't has an active session
        //Create operation
        if (hasActiveSession.Id == 0)
        {
            //allow the hit to be initiated
            var siteFromDb = await _siteRepository
                .GetSiteByIdAsync(hit.SiteId);

            var requestDomain = HttpContext.Request.Host.Host;

            if (requestDomain != siteFromDb.Domain)
            {
                return BadRequest();
            }

            var result = await _hitRepository.CreateAsync(hit);

            if (result is null)
            {
                return BadRequest();
            }

            await _sessionRepository.CreateAsync(new SessionDto
            {
                DeviceId = hit.DeviceId
            });

            return Ok(result);
        }

        //check if the session has active last hit or not
        //Update operation
        var sessionLastHit = hasActiveSession.LastDeviceHit;

        var timeInLast10Minutes = DateTime.UtcNow - new TimeSpan(0, 10, 0);

        if (DateTime.Compare(timeInLast10Minutes, sessionLastHit) > 0)
        {
            //allow the hit to be created
            var siteFromDb = await _siteRepository.GetSiteByIdAsync(hit.SiteId);

            var requestDomain = HttpContext.Request.Host.Host;

            if (requestDomain != siteFromDb.Domain) return BadRequest();

            var result = await _hitRepository.CreateAsync(hit);

            if (result is null) return BadRequest();

            await _sessionRepository.UpdateAsync(hasActiveSession);

            return Ok(result);
        }

        //Reject the hit since the session is still active
        return BadRequest();
    }
}
