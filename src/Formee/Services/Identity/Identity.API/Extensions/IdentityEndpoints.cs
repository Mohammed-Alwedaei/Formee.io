using Identity.BusinessLogic.Dtos;
using Identity.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Mvc;
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
                UpsertUserDto user) =>
        {
            var result = await identityManager.CreateAsync(user);

            if (result == null)
            {
                return Results.BadRequest();
            }

            var createUserResponse = await subscriptionsClient
                .CreateUserAndAssignSubscriptionAsync(result.Id, result.Email);

            return createUserResponse.SubscriptionId != 0 
                ? Results.Ok(result) 
                : Results.BadRequest();
        }).AllowAnonymous();

        identity.MapPut("/users", async
        (IIdentityManager identityManager, UpsertUserDto user) =>
        {
            if (user == null)
            {
                return Results.BadRequest();
            }

            var result = await identityManager.UpdateAsync(user);

            return result == null 
                ? Results.Problem(statusCode: StatusCodes.Status500InternalServerError)
                : Results.Ok(result);
        }).AllowAnonymous();


        //Get token
        identity.MapGet("/users/token/{userId}", async
            (IJwtManager jwtManager, string userId) =>
        {
            var result = await jwtManager.GetAsync(userId);

            return Results.Ok(result);
        }).AllowAnonymous();

        identity.MapPost("/users/avatar/{userId:Guid}", async
            (IIdentityManager identityService, IFormFileCollection avatar, Guid userId)
            => Results.Ok(await identityService
                .UploadUserAvatar(avatar, userId))).AllowAnonymous();

        return app;
    }
}
