using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.BusinessLogic.Dtos.Subscriptions;
using Subscriptions.BusinessLogic.Repositories.IRepository;

namespace Subscriptions.API.Controllers;

[ApiController]
[Authorize(Policy = "users")]
[Route("api/subscriptions/features")]
public class SubscriptionFeaturesController : ControllerBase
{
    private readonly ISubscriptionFeatureRepository 
        _subscriptionFeatureRepository;
    private readonly ILogger<SubscriptionFeaturesController> _logger;

    public SubscriptionFeaturesController(
        ISubscriptionFeatureRepository subscriptionFeatureRepository, 
        ILogger<SubscriptionFeaturesController> logger)
    {
        _subscriptionFeatureRepository = subscriptionFeatureRepository;
        _logger = logger;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserSubscriptionById(int id)
    {
        _logger.LogInformation("GET: request at /api/subscriptions/features/{id} at {datetime}",
            id,
            DateTime.Now);

        if (id is 0) return BadRequest();

        var result = await _subscriptionFeatureRepository.GetOneByIdAsync(id);

        if (result is null) return NotFound();

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("GET: request at /api/subscriptions/features at {datetime}",
            DateTime.Now);

        var result = await _subscriptionFeatureRepository.GetAllAsync();

        if (!result.Any())
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFeature
        (SubscriptionFeatureDto feature)
    {
        _logger.LogInformation("POST: request /api/subscriptions/features at {dateTime}", 
            DateTime.Now);

        if(!ModelState.IsValid) return BadRequest(feature);

        var createdFeature = await _subscriptionFeatureRepository
            .CreateAsync(feature);

        return Ok(createdFeature);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFeature
        (SubscriptionFeatureDto feature)
    {
        _logger.LogInformation("POST: request /api/subscriptions/features at {dateTime}",
            DateTime.Now);

        if (!ModelState.IsValid) return BadRequest(feature);

        var updatedFeature = await _subscriptionFeatureRepository
            .UpdateAsync(feature);

        return Ok(updatedFeature);
    }

    [HttpDelete("{featureId:int}")]
    public async Task<IActionResult> DeleteFeature(int featureId)
    {
        _logger.LogInformation("DELETE: request /api/subscriptions/features at {dateTime}",
            DateTime.Now);

        var deletedFeature = await _subscriptionFeatureRepository
            .DeleteAsync(featureId);

        if (deletedFeature.Id is 0)
            return NotFound();

        return Ok(deletedFeature);
    }
}
