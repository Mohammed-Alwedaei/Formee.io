using Analytics.Utilities.Dtos.Category;

namespace Analytics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryController> _logger;
    public CategoryController(ICategoryRepository categoryRepository, 
        ILogger<CategoryController> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
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

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        _logger.LogInformation("PUT: request at /api/categories at {datetime}",
            DateTime.Now);

        var result = await _categoryRepository
            .DeleteAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
