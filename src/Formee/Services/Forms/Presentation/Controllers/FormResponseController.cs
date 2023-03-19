using Application.FormResponse.Commands.CreateFormResponse;
using Application.FormResponse.Query.GetAllByFormId;
using Application.FormResponse.Commands.DeleteFormResponse;
using Application.FormResponse.Query.GetFormResponseById;

namespace Presentation.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class FormResponseController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FormResponseController> _logger;

    public FormResponseController(IMediator mediator,
        ILogger<FormResponseController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetFormResponseById(int id)
    {
        _logger.LogInformation(
            "GET: request to route /api/forms/responses at " +
            "{datetime}", DateTime.Now);

        var result = await _mediator.Send(new
            GetFormResponseByIdQuery(id));

        return Ok(result);
    }

    [HttpGet("all/{formId:int}")]
    public async Task<IActionResult> GetAllFormResponseByFormId(int formId)
    {
        _logger.LogInformation(
            "GET: request to route /api/forms/responses at " +
            "{datetime}", DateTime.Now);

        var result = await _mediator.Send(new
            GetAllResponsesByFormIdQuery(formId));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFormResponse(
        FormResponseEntity form)
    {
        _logger.LogInformation(
            "POST: request to route /api/fields at " +
            "{datetime}", DateTime.Now);

        var result = await _mediator.Send(new
            CreateFormResponseCommand(form));

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteFormResponseById(int id)
    {
        _logger.LogInformation(
            "DELETE: request to route /api/forms/responses at " +
            "{datetime}", DateTime.Now);

        var result = await _mediator.Send(new
            DeleteFormResponseCommand(id));

        return Ok(result);
    }
}
