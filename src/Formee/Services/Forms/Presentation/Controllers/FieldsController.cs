using Application.Fields.Commands.Create;
using Application.Fields.Commands.DeleteById;
using Application.Fields.Commands.UpdateById;
using Application.Fields.Queries.GetAllByUserId;
using Application.Fields.Queries.GetById;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers;

[ApiController]
[Authorize(Policy = "users")]
[Route("api/[controller]/")]
public class FieldsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FieldsController> _logger;

    public FieldsController(IMediator mediator, 
        ILogger<FieldsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetFieldById(int id)
    {
        _logger.LogInformation(
            "GET: request to route /api/fields/{id} at " +
            "{datetime}", id, DateTime.Now);

        var result = await _mediator
            .Send(new GetFieldByIdQuery(id));

        return Ok(result);
    }

    [HttpGet("all/{formId:int}")]
    public async Task<IActionResult> GetAllFieldsByFormId(int formId)
    {
        _logger.LogInformation(
            "GET: request to route /api/fields/{formId} at " +
            "{datetime}", formId, DateTime.Now);

        var result = await _mediator
            .Send(new GetAllByFormIdQuery(formId));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateField(FieldEntity field)
    {
        _logger.LogInformation(
            "POST: request to route /api/fields at " +
            "{datetime}", DateTime.Now);

        var result = await _mediator.Send(new
            CreateFieldCommand(field));

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateField(FieldEntity field)
    {
        _logger.LogInformation(
            "PUT: request to route /api/fields at " +
            "{datetime}", DateTime.Now);

        var result = await _mediator.Send(new
            UpdateFieldByIdCommand(field));

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteFormById(int id)
    {
        _logger.LogInformation(
            "DELETE: request to route /api/fields at " +
            "{datetime}", DateTime.Now);

        var result = await _mediator.Send(new
            DeleteFieldByIdCommand(id));

        return Ok(result);
    }
}
