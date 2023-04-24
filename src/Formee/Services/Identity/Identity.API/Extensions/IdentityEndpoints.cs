using Identity.BusinessLogic.Dtos;
using Identity.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using ServiceBus.Constants;
using ServiceBus.Messages;
using ServiceBus.Models;
using ServiceBus.ServiceBus;
using SynchronousCommunication.HttpClients;

namespace Identity.API.Extensions;

public static class IdentityEndpoints
{
    public static WebApplication UseIdentityEndpoints(this WebApplication app)
    {
        var identity = app.MapGroup("/api/identity")
                .RequireAuthorization()
                .WithTags("Users");

        var logger = app.Logger;

        identity.MapGet("/users/{userId:Guid}", async
            (IIdentityManager identityManager, Guid userId) =>
        {
            logger.LogInformation("GET: request at /api/identity/users/{userId} at {date}",
                userId, DateTime.UtcNow);

            var user = await identityManager.GetByIdAsync(userId);

            return string.IsNullOrEmpty(user.AuthId)
                ? Results.NotFound() 
                : Results.Ok(user);
        });

        identity.MapGet("/users/{authId}", async
            (IIdentityManager identityManager, string authId) =>
        {
            logger.LogInformation("GET: request at /api/identity/users/{auth} at {date}",
                authId, DateTime.UtcNow);

            var user = await identityManager.GetByAuthIdAsync(authId);

            return string.IsNullOrEmpty(user.AuthId)
                ? Results.NotFound()
                : Results.Ok(user);
        }).AllowAnonymous(); 

        identity.MapGet("/users/all", async
            (IIdentityManager identityManager, [FromQuery] string filter) =>
        {
            var user = await identityManager.GetAllByFilterAsync(filter);

            return user.Count is 0
                ? Results.NotFound()
                : Results.Ok(user);
        });

        //Assign Admin to a user
        identity.MapPost("/admins/{roleId}", async
            (IIdentityManager identityService, AddRoleToUseDto users, string roleId) =>
        {
            var result = await identityService.AssignRoleToUser(users, roleId);

            return result ? Results.Ok(result) : Results.BadRequest();
        }).WithTags("Admins");

        //Create user
        identity.MapPost("/users", async
            (IIdentityManager identityManager, 
                ISubscriptionsClient subscriptionsClient, 
                IAzureServiceBus<HistoryMessage> historyServiceBus,
                IAzureServiceBus<NotificationMessage> notificationServiceBus,
                UpsertUserDto user) =>
        {
            var result = await identityManager.CreateAsync(user);

            if (result.Id == Guid.Empty)
                return Results.Problem(statusCode: 500);

            var createUserResponse = await subscriptionsClient
                .CreateUserAndAssignSubscriptionAsync(result.Id, result.Email);

            await notificationServiceBus.SendMessage(new NotificationModel
            {
                GlobalUserId = result.Id,
                Heading = $"{result.UserName} account is created",
                Message = $"You have registered {result.UserName} account"
            });

            await historyServiceBus.SendMessage(new HistoryModel
            {
                Title = $"{result.UserName} account is registered",
                Action = ActionType.Create,
                UserId = result.Id,
                Service = Services.Identity
            });

            return createUserResponse.SubscriptionId != 0 
                ? Results.Ok(result) 
                : Results.BadRequest();
        }).AllowAnonymous();

        identity.MapPut("/users", async
        (IIdentityManager identityManager, 
            IAzureServiceBus<HistoryMessage> historyServiceBus,
            IAzureServiceBus<NotificationMessage> notificationServiceBus,
            UpsertUserDto user) =>
        {
            if (user == null)
            {
                return Results.BadRequest();
            }

            var result = await identityManager.UpdateAsync(user);

            if (result.Id == Guid.Empty)
                return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);

            await notificationServiceBus.SendMessage(new NotificationModel
            {
                GlobalUserId = result.Id,
                Heading = $"{result.UserName} account is updated",
                Message = $"You have updated {result.UserName} account"
            });

            await historyServiceBus.SendMessage(new HistoryModel
            {
                Title = $"{result.UserName} account is updated",
                Action = ActionType.Update,
                UserId = result.Id,
                Service = Services.Identity
            });

            return Results.Ok(result);
        }).AllowAnonymous();


        //Get token
        identity.MapGet("/users/token/{userId}", async
            (IJwtManager jwtManager, string userId) =>
        {
            var result = await jwtManager.GetAsync(userId);

            return Results.Ok(result);
        }).AllowAnonymous();

        identity.MapPost("/users/avatar/{userId:Guid}", async
            (IIdentityManager identityService,
                IAzureServiceBus<HistoryMessage> historyServiceBus,
                IAzureServiceBus<NotificationMessage> notificationServiceBus,
                IFormFileCollection avatar, Guid userId) =>
        {

            var result = await identityService
                .UploadUserAvatar(avatar, userId);

            if(result.Id is 0)
                return Results
                .Problem(statusCode: StatusCodes.Status500InternalServerError);

            await notificationServiceBus.SendMessage(new NotificationModel
            {
                GlobalUserId = result.UserId,
                Heading = "Account avatar is updated",
                Message = "You have updated account avatar"
            });

            await historyServiceBus.SendMessage(new HistoryModel
            {
                Title = $"Account avatar is updated",
                Action = ActionType.Update,
                UserId = result.UserId,
                Service = Services.Identity
            });

            return Results.Ok();
        }).AllowAnonymous();

        return app;
    }
}
