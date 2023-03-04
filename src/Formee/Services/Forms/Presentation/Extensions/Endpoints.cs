using MediatR;
using Domain.Entities;
using Application.Forms.Queries.GetById;
using Application.Forms.Commands.Create;
using Application.Fields.Queries.GetById;
using Application.Forms.Commands.DeleteById;
using Application.Forms.Commands.UpdateById;
using Application.Forms.Queries.GetAllByFormId;
using Application.Fields.Queries.GetAllByUserId;
using Application.Fields.Commands.Create;
using Application.Fields.Commands.UpdateById;
using Application.Fields.Commands.DeleteById;

namespace Presentation.Extensions;

/// <summary>
/// Register Forms endpoints
/// </summary>
public static class Endpoints
{
    /// <summary>
    /// Add Forms API endpoints to the web application
    /// </summary>
    /// <param name="app"></param>
    /// <returns>web application with api endpoints</returns>
    public static WebApplication UseFormsEndpoints(this WebApplication app)
    {
        var forms = app
            .MapGroup("/api/forms")
            .WithTags("Forms")
            .AllowAnonymous();
            // .RequireAuthorization();

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

            return Results.Ok(result);
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

            return Results.Ok(result);
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

            return Results.Ok(result);
        });

        return app;
    }

    public static WebApplication UseFieldsEndpoints(this WebApplication app)
    {
        var fields = app
            .MapGroup("/api/fields")
            .WithTags("Fields")
            .AllowAnonymous();
            // .RequireAuthorization();

        var logger = app.Logger;

        fields.MapGet("/{id:int}", async
            (IMediator mediator, int id) =>
        {
            logger.LogInformation(
                "GET: request to route /api/fields/{id} at " +
                "{datetime}", id, DateTime.Now);

            var result = await mediator
                .Send(new GetFieldByIdQuery(id));

            return Results.Ok(result);
        });

        fields.MapGet("/all/{formId:int}", async
            (IMediator mediator, int formId) =>
        {
            logger.LogInformation(
                "GET: request to route /api/fields/{formId} at " +
                "{datetime}", formId, DateTime.Now);

            var result = await mediator
                .Send(new GetAllByFormIdQuery(formId));

            return Results.Ok(result);
        });

        fields.MapPost("/", async
            (IMediator mediator, FieldEntity field) =>
        {
            logger.LogInformation(
                "POST: request to route /api/fields at " +
                "{datetime}", DateTime.Now);

            var result = await mediator.Send(new
                CreateFieldCommand(field));

            return Results.Ok(result);
        });

        fields.MapPut("/", async
            (IMediator mediator, FieldEntity field) =>
        {
            logger.LogInformation(
                "PUT: request to route /api/fields at " +
                "{datetime}", DateTime.Now);

            var result = await mediator.Send(new
                UpdateFieldByIdCommand(field));

            return Results.Ok(result);
        });

        fields.MapDelete("/{id:int}", async
            (IMediator mediator, int id) =>
        {
            logger.LogInformation(
                "PUT: request to route /api/fields at " +
                "{datetime}", DateTime.Now);

            var result = await mediator.Send(new
                DeleteFieldByIdCommand(id));

            return Results.Ok(result);
        });

        return app;
    }

    public static WebApplication UseErrorEndpoints(this WebApplication app)
    {
        app.Map("/error", () => Results.Problem());

        return app;
    }

}
