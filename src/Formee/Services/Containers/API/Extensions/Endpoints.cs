using Microsoft.AspNetCore.Mvc;
using SynchronousCommunication.HttpClients;

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
                ISubscriptionsClient subscriptionClient,
                IAzureServiceBus<HistoryMessage> serviceBus,
                ContainerEntity container) =>
            {
                //Check if the user can create a container
                var subscriptionFeatures = await subscriptionClient.GetSubscriptionFeaturesAsync(container.UserId);
                
                var numberOfUserContainers = await containersService
                    .GetAllContainerByUserIdAsync(container.UserId);

                var numberOfRemainingContainers =
                    subscriptionFeatures.Subscription.SubscriptionFeatures.NumberOfContainers - numberOfUserContainers.Count;

                if (numberOfRemainingContainers is 0)
                {
                    return Results.Problem(statusCode: 403);
                }
                
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
                ContainerDto container) =>
            {
                if (string.IsNullOrEmpty(container.Id))
                    return Results.BadRequest();

                var result = await containersService
                    .UpdateContainerAsync(container);

                if (!string.IsNullOrEmpty(result.Id))
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

                    return Results.Ok();
                }

                return Results.NotFound();
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
                var result = await containersService
                    .DeleteContainerAsync(id);

                if (result is not null)
                {
                    var history = new HistoryModel
                    {
                        Title = "A new container is deleted",
                        Action = ActionType.Delete,
                        UserId = result.UserId,
                        Service = SystemServices.Containers
                    };

                    var notification = new NotificationModel
                    {
                        GlobalUserId = result.UserId,
                        Heading = "A container is Deleted",
                        Message = "The container of name ... is deleted"
                    };

                    await serviceBus.SendMessage(
                        new NotificationMessage
                        {
                            Entity = notification
                        });

                    Results.Ok(result);
                }
                else
                {
                    Results.NotFound();
                }
            });
        return app;
    }
}
