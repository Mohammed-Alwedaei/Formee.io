using Identity.BusinessLogic.Models;
using Identity.BusinessLogic.Services;

namespace Identity.API.Extensions;

public static class IdentityEndpoints
{
    public static WebApplication UseIdentityEndpoints(this WebApplication app)
    {
        var identity = app.MapGroup("/api/identity");

        //Assign Admin to a user
        identity.MapPost("/admin/{roleId}", async
            (IdentityManager identityService, AddRoleToUserModel users, string roleId) =>
        {
            var result = await identityService.AssignRoleToUser(users, roleId);

            return Results.Ok(result);
        }).WithTags("Admins");

        identity.MapPost("/users/avatar/{userId:Guid}", async
            (IdentityManager identityService, IFormFile file, Guid userId) =>
        {

            return Results.Ok(await identityService
                .UploadUserAvatar(file, userId));
        }).WithTags("Users");

        return app;
    }
}
