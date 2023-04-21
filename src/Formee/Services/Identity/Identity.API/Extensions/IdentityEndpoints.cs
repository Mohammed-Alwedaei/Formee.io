using Identity.BusinessLogic.Dtos;
using Identity.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Extensions;

public static class IdentityEndpoints
{
    public static WebApplication UseIdentityEndpoints(this WebApplication app)
    {
        var identity = app.MapGroup("/api/identity")
            .WithTags("Users");

        identity.MapGet("/users/{userId:Guid}", async
            (IIdentityManager identityManager, Guid userId) =>
        {
            var user = await identityManager.GetByIdAsync(userId);

            return string.IsNullOrEmpty(user.AuthId)
                ? Results.NotFound() 
                : Results.Ok(user);
        });

        identity.MapGet("/users/{authId}", async
            (IIdentityManager identityManager, string authId) =>
        {
            var user = await identityManager.GetByAuthIdAsync(authId);

            return string.IsNullOrEmpty(user.AuthId)
                ? Results.NotFound()
                : Results.Ok(user);
        });

        identity.MapGet("/users/all", async
            (IIdentityManager identityManager, [FromQuery] string filter) =>
        {
            var user = await identityManager.GetAllByFilterAsync(filter);

            return user.Count is 0
                ? Results.NotFound()
                : Results.Ok(user);
        });

        //Assign Admin to a user
        identity.MapPost("/admin/{roleId}", async
            (IIdentityManager identityService, AddRoleToUseDto users, string roleId) =>
        {
            var result = await identityService.AssignRoleToUser(users, roleId);

            return Results.Ok(result);
        }).WithTags("Admins");

        identity.MapPost("/users", async 
            (IIdentityManager identityManager, CreateUserDto user) =>
        {
            var result = await identityManager.CreateAsync(user);

            return Results.Ok(result);
        });

        identity.MapPost("/users/avatar/{userId:Guid}", async
            (IIdentityManager identityService, IFormFileCollection avatar, Guid userId)
            => Results.Ok(await identityService
                .UploadUserAvatar(avatar, userId)));

        return app;
    }
}
