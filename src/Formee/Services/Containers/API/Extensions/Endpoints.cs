using API.Entities;
using API.Services;

namespace API.Extensions;

public static class Endpoints
{
    public static WebApplication UseContainerEndpoints(this WebApplication app)
    {
        var containers = app.MapGroup("/api/containers");

        /*
         * Route: /api/container/
         * DESC : Get a single container that match the id
         * Auth : Users
         */
        containers.MapGet("/{id}",
            async (ContainersService containersService, string id) =>
            {
                if (await containersService.GetContainerById(id)
                    is ContainerEntity containers)
                {
                    return Results.Ok(containers);
                }

                return Results.NotFound();
            });

        /*
         * Route: /api/container/all/userId
         * DESC : Get all containers that are not deleted and match the user id
         * Auth : Users
         */
        containers.MapGet("/all/{userId}",
            async (ContainersService containersService, Guid userId) =>
            {
                if (await containersService.GetAllContainerByUserIdAsync(userId)
                    is List<ContainerEntity> containers)
                {
                    return Results.Ok(containers);
                }

                return Results.NotFound();
            });

        /*
         * Route: /api/container/
         * DESC : Insert container in the database and notify subscribers (gRPC Client)
         * Auth : Users
         */
        containers.MapPost("/",
            async (ContainersService containersService,
                 ContainerEntity container) =>
            {
                if (await containersService.CreateContainerAsync(container)
                    is ContainerEntity containerResult)
                {
                    return Results.Ok(containerResult);
                }

                return Results.BadRequest();
            });

        /*
         * Route: /api/container/
         * DESC : Update a whole container in the database
         * Auth : Users
         */
        containers.MapPut("/",
            async (ContainersService containersService,
                    ContainerEntity container) =>
                Results.Ok(await containersService
                    .UpdateContainerAsync(container)));

        /*
         * Route: /api/container/id
         * DESC : Delete container in the database
         * Auth : Users
         */
        containers.MapDelete("/{id:length(24)}",
            async (ContainersService containersService, string id) =>
            {
                var results = await containersService
                    .DeleteContainerAsync(id);

                return results ? Results.Ok(id) : Results.NotFound();
            });
        return app;
    }
}
