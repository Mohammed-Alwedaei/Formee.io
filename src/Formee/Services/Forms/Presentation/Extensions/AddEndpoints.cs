using MediatR;
using Domain.Entities;
using Application.Forms.Queries.GetById;
using Application.Forms.Commands.Create;
using Application.Forms.Commands.DeleteById;
using Application.Forms.Commands.UpdateById;
using Application.Forms.Queries.GetAllByUserId;

namespace Presentation.Extensions;

/// <summary>
/// Register Forms endpoints
/// </summary>
public static class AddEndpoints
{
    /// <summary>
    /// Add Forms API endpoints to the web application
    /// </summary>
    /// <param name="app"></param>
    /// <returns>web application with api endpoints</returns>
    public static WebApplication AddFormsEndpoints(this WebApplication app)
    {
        var forms = app.MapGroup("/api/forms");
        var logger = app.Logger;

        /*
         * Route: /api/forms/userId
         * Desc : get all entities for a user
         * Auth : Users
         */
        forms.MapGet("/{userId:Guid}", async
            (IMediator mediator, Guid userId) =>
        {
            logger.LogInformation(
                "GET: request to route /api/forms/{userId} at " +
                "{datetime}", userId, DateTime.Now);

            var result = await mediator
                .Send(new GetAllByUserIdQuery(userId));

            if (result.IsSuccessRequest is not true) return Results.NotFound();

            return Results.Ok(result);
        });

        /*
         * Route: /api/forms/id
         * Desc : get a single entity by id
         * Auth : Users
         */
        forms.MapGet("/{id:int}", async
            (IMediator mediator, int id) =>
        {
            logger.LogInformation(
                "GET: request to route /api/forms/{id} at " +
                "{datetime}", id, DateTime.Now);

            var result = await mediator
                .Send(new GetFormByIdQuery(id));

            if(result is null) return Results.NotFound();

            return Results.Ok(result);
        });

        /*
         * Route: /api/forms
         * Desc : create a form for a user
         * Auth : Users
         */
        forms.MapPost("/", async
            (IMediator mediator, FormEntity form) =>
        {
            logger.LogInformation(
                "POST: request to route /api/forms at " +
                "{datetime}", DateTime.Now);

            var result = await mediator.Send(new
                CreateFormCommand(form));

            return result.IsSuccessRequest is not true 
                ? Results.Problem() 
                : Results.Ok(result);
        });

        /*
         * Route: /api/forms
         * Desc : update a form for a user
         * Auth : Users
         */
        forms.MapPut("/", async
            (IMediator mediator, FormEntity form) =>
        {
            logger.LogInformation(
                "PUT: request to route /api/forms at " +
                "{datetime}", DateTime.Now);

            var result = await mediator.Send(new
                UpdateFormByIdCommand(form));

            return result.IsSuccessRequest is not true 
                ? Results.Problem() 
                : Results.Ok(result);
        });

        /*
         * Route: /api/forms/id
         * Desc : delete a form of a user
         * Auth : Users
         */
        forms.MapDelete("/{id:int}", async
            (IMediator mediator, int id) =>
        {
            logger.LogInformation(
                "DELETE: request to route /api/forms/{ID} at " +
                "{datetime}", 
                id, 
                DateTime.Now);

            var result = await mediator.Send(new
                DeleteByIdCommand(id));

            return result.IsSuccessRequest is not true
                ? Results.Problem()
                : Results.Ok(result);
        });

        return app;
    }
}
