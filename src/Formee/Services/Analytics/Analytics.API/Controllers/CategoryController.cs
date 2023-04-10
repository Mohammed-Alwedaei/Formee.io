using Analytics.Utilities.Dtos.Category;
using ServiceBus.Constants;
using ServiceBus.Messages;
using ServiceBus.Models;
using ServiceBus.ServiceBus;

namespace Analytics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryController> _logger;
    private readonly IAzureServiceBus<HistoryMessage> _serviceBus;
    public CategoryController(ICategoryRepository categoryRepository, 
        ILogger<CategoryController> logger,
        IAzureServiceBus<HistoryMessage> serviceBus)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
        _serviceBus = serviceBus;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        _logger.LogInformation("GET: request at /api/categories/{id} at {datetime}",
        id, DateTime.Now);

        var result = await _categoryRepository.GetByIdAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCategoriesById()
    {
        _logger.LogInformation("GET: request at /api/categories/all at {datetime}",
            DateTime.Now);

        var result = await _categoryRepository.GetAllAsync();

        if (result.Count is 0)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory
        (CreateCategoryDto category)
    {
        _logger.LogInformation("POST: request at /api/categories at {datetime}",
            DateTime.Now);

        var result = await _categoryRepository
            .CreateAsync(category);

        if (result is null)
        {
            return BadRequest();
        }

        var history = new HistoryModel
        {
            Title = "A new category is created",
            Action = ActionType.Create,
            UserId = category.UserId,
            Service = SystemServices.Analytics
        };

        await _serviceBus.SendMessage(new HistoryMessage
        {
            Entity = history
        });

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory
        (UpdateCategoryDto category)
    {
        _logger.LogInformation("PUT: request at /api/categories at {datetime}",
            DateTime.Now);

        var result = await _categoryRepository
            .UpdateAsync(category);

        if (result is null)
        {
            return NotFound();
        }

        var history = new HistoryModel
        {
            Title = "A category is updated",
            Action = ActionType.Update,
            UserId = category.UserId,
            Service = SystemServices.Analytics
        };

        await _serviceBus.SendMessage(new HistoryMessage
        {
            Entity = history
        }); ;

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        _logger.LogInformation("PUT: request at /api/categories at {datetime}",
            DateTime.Now);

        var result = await _categoryRepository
            .DeleteAsync(id);

        if (result.Id is 0)
        {
            return NotFound();
        }

        var history = new HistoryModel  
        {
            Title = "A category is deleted",
            Action = ActionType.Delete,
            UserId = result.UserId,
            Service = SystemServices.Analytics
        };

        await _serviceBus.SendMessage(new HistoryMessage
        {
            Entity = history
        });

        return Ok(result);
    }
}
