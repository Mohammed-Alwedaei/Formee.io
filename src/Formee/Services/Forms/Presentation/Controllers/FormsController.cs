using Application.Forms.Queries.GetById;
using Application.Forms.Commands.Create;
using Application.Forms.Commands.DeleteById;
using Application.Forms.Commands.UpdateById;
using Application.Forms.Queries.GetAllByFormId;

namespace Presentation.Controllers;

[ApiController]
[Tags("Forms")]
[Route("api/[controller]/")]
public class FormsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FormsController> _logger;

    public FormsController(IMediator mediator, ILogger<FormsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /*
    * Route: /api/forms/id
    * Desc : get a single entity by id
    * Auth : Users
    */
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetFormById(int id)
    {
        _logger.LogInformation(
            "GET: request to route /api/forms/{id} at " +
            "{datetime}", id, DateTime.Now);

        var result = await _mediator
            .Send(new GetFormByIdQuery(id));

        return Ok(result);
    }

     /*
     * Route: /api/forms/userId
     * Desc : get all entities for a user
     * Auth : Users
     */
    [HttpGet]
    public async Task<IActionResult> GetAllFormsByUserId(Guid userId)
    {
        _logger.LogInformation(
            "GET: request to route /api/forms/{userId} at " +
            "{datetime}", userId, DateTime.Now);

        var result = await _mediator
            .Send(new GetAllByUserIdQuery(userId));

        return Ok(result);
    }

     /*
     * Route: /api/forms
     * Desc : create a form for a user
     * Auth : Users
     */
    [HttpPost]
    public async Task<IActionResult> CreateForm(FormEntity form)
    {
        _logger.LogInformation(
            "POST: request to route /api/forms at " +
            "{datetime}", DateTime.Now);

        var result = await _mediator.Send(new
            CreateFormCommand(form));

        return Ok(result);
    }

     /*
     * Route: /api/forms/id
     * Desc : delete a form of a user
     * Auth : Users
     */
    [HttpPut]
    public async Task<IActionResult> UpdateForm(FormEntity form)
    {
        _logger.LogInformation(
            "PUT: request to route /api/forms at " +
            "{datetime}", DateTime.Now);

        var result = await _mediator.Send(new
            UpdateFormByIdCommand(form));

        return Ok(result);
    }

    /*
     * Route: /api/forms/id
     * Desc : delete a form of a user
     * Auth : Users
     */
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteFormById(int id)
    {
        _logger.LogInformation(
            "DELETE: request to route /api/forms/{ID} at " +
            "{datetime}",
            id,
            DateTime.Now);

        var result = await _mediator.Send(new
            DeleteByIdCommand(id));

        return Ok(result);
    }
}
