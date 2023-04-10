using API.Entities;
using API.Services;
using ServiceBus.Constants;
using ServiceBus.Messages;
using ServiceBus.Models;
using ServiceBus.ServiceBus;

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
                IAzureServiceBus<HistoryMessage> serviceBus,
                ContainerEntity container) =>
            {
                if (await containersService.CreateContainerAsync(container)
                    is ContainerEntity containerResult)
                {
                    var history = new HistoryModel
                    {
                        Title = "A new container is created",
                        Action = ActionType.Create,
                        UserId = container.UserId,
                        Service = SystemServices.Containers
                    };

                    await serviceBus.SendMessage(
                        new HistoryMessage
                        {
                            Entity = history
                        });

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
                IAzureServiceBus<HistoryMessage> serviceBus,
                ContainerEntity container) =>
            {
                if (await containersService
                        .UpdateContainerAsync(container))
                {
                    var history = new HistoryModel
                    {
                        Title = "A container is updated",
                        Action = ActionType.Update,
                        UserId = container.UserId,
                        Service = SystemServices.Containers
                    };

                    await serviceBus.SendMessage(
                        new HistoryMessage
                        {
                            Entity = history
                        });

                    Results.Ok();
                }
                else
                {
                    Results.BadRequest();
                }
            });

        /*
         * Route: /api/container/id
         * DESC : Delete container in the database
         * Auth : Users
         */
        containers.MapDelete("/{id:length(24)}",
            async (ContainersService containersService,
                IAzureServiceBus<NotificationMessage> serviceBus,
                string id) =>
            {
                var results = await containersService
                    .DeleteContainerAsync(id);

                if (results is not null)
                {
                    var history = new HistoryModel
                    {
                        Title = "A new container is deleted",
                        Action = ActionType.Delete,
                        UserId = results.UserId,
                        Service = SystemServices.Containers
                    };

                    var notification = new NotificationModel
                    {
                        GlobalUserId = results.UserId,
                        Heading = "A container is Deleted",
                        Message = "The container of name ... is deleted"
                    };

                    await serviceBus.SendMessage(
                        new NotificationMessage
                        {
                            Entity = notification
                        });

                    Results.Ok(id);
                }
                else
                {
                    Results.NotFound();
                }
            });
        return app;
    }
}
