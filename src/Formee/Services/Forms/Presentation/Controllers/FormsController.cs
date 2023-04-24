using Application.Forms.Queries.GetById;
using Application.Forms.Commands.Create;
using Application.Forms.Commands.DeleteById;
using Application.Forms.Commands.UpdateById;
using Application.Forms.Queries.GetAllBySiteId;
using Microsoft.AspNetCore.Authorization;
using ServiceBus.Messages;
using SynchronousCommunication.HttpClients;
using ServiceBus.ServiceBus;
using ServiceBus.Constants;
using ServiceBus.Models;
using Domain.Exceptions;

namespace Presentation.Controllers;

[ApiController]
[Tags("Forms")]
[Authorize]
[Route("api/[controller]/")]
public class FormsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FormsController> _logger;
    private readonly IAzureServiceBus<HistoryMessage> _historyServiceBus;
    private readonly IAzureServiceBus<NotificationMessage> 
        _notificationServiceBus;
    private readonly ISubscriptionsClient _subscriptionsClient;

    public FormsController(IMediator mediator, 
        ILogger<FormsController> logger, 
        ISubscriptionsClient subscriptionsClient,
        IAzureServiceBus<HistoryMessage> historyServiceBus, 
        IAzureServiceBus<NotificationMessage> notificationServiceBus)
    {
        _mediator = mediator;
        _logger = logger;
        _subscriptionsClient = subscriptionsClient;
        _historyServiceBus = historyServiceBus;
        _notificationServiceBus = notificationServiceBus;
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
    [HttpGet("all/{siteId:int}")]
    public async Task<IActionResult> GetAllFormsByUserId(int siteId)
    {
        _logger.LogInformation(
            "GET: request to route /api/forms/all/{siteId} at " +
            "{datetime}", siteId, DateTime.Now);

        var result = await _mediator
            .Send(new GetAllBySiteIdQuery(siteId));

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

        var subscriptionFeatures = await _subscriptionsClient
            .GetSubscriptionFeaturesAsync(form.UserId);

        List<FormEntity> numberOfUserForms;

        try
        {
            numberOfUserForms = await _mediator
                .Send((new GetAllBySiteIdQuery(form.SiteId)));
        }
        catch (NotFoundException exception)
        {
            numberOfUserForms = new List<FormEntity>();
        }

        var numberOfRemainingForms = subscriptionFeatures.Subscription
            .SubscriptionFeatures.NumberOfForms - numberOfUserForms.Count;

        if (numberOfRemainingForms is 0) return Forbid();

        var result = await _mediator.Send(new
            CreateFormCommand(form));

        if (result is not { }) return Problem(statusCode: 500);

        await _notificationServiceBus.SendMessage(new NotificationModel
        {
            GlobalUserId = form.UserId,
            Heading = $"{form.Details.Name} form is created",
            Message = $"You have created {form.Details.Name} form"
        });

        await _historyServiceBus.SendMessage(new HistoryModel
        {
            Title = $"{form.Details.Name} form is created",
            Action = ActionType.Create,
            UserId = form.UserId,
            Service = Services.Forms
        });

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

        if (result is not { }) return Problem(statusCode: 500);

        await _notificationServiceBus.SendMessage(new NotificationModel
        {
            GlobalUserId = form.UserId,
            Heading = $"{form.Details.Name} form is updated",
            Message = $"You have updated {form.Details.Name} form"
        });

        await _historyServiceBus.SendMessage(new HistoryModel
        {
            Title = $"{form.Details.Name} form is updated",
            Action = ActionType.Update,
            UserId = form.UserId,
            Service = Services.Forms
        });

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

        if (result is not { }) return Problem(statusCode: 500);

        await _notificationServiceBus.SendMessage(new NotificationModel
        {
            GlobalUserId = result.UserId,
            Heading = $"{result.Details.Name} form is deleted",
            Message = $"You have deleted {result.Details.Name} form"
        });

        await _historyServiceBus.SendMessage(new HistoryModel
        {
            Title = $"{result.Details.Name} form is deleted",
            Action = ActionType.Delete,
            UserId = result.UserId,
            Service = Services.Forms
        });

        return Ok(result);
    }
}
