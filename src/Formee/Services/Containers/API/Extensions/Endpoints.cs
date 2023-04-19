﻿using SynchronousCommunication.HttpClients;

namespace API.Extensions;

public static class Endpoints
{
    public static WebApplication UseContainerEndpoints(this WebApplication app)
    {
        var containersRouteGroup = app.MapGroup("/api/containers");

        /*
         * Route: /api/container/
         * DESC : Get a single container that match the id
         * Auth : Users
         */
        containersRouteGroup.MapGet("/{id}",
            async (ContainersService containersService, string id) =>
            {
                if (await containersService.GetContainerById(id) is { } container) return Results.Ok(container);
                
                return Results.NotFound();
            });

        /*
         * Route: /api/container/all/userId
         * DESC : Get all containers that are not deleted and match the user id
         * Auth : Users
         */
        containersRouteGroup.MapGet("/all/{userId}",
            async (ContainersService containersService, Guid userId) =>
            {
                if (await containersService.GetAllContainerByUserIdAsync(userId) is { } containers)
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
        containersRouteGroup.MapPost("/",
            async (ContainersService containersService,
                ISubscriptionsClient subscriptionClient,
                IAzureServiceBus<HistoryMessage> serviceBus,
                IAzureServiceBus<NotificationMessage> notificationServiceBus,
                ContainerEntity container) =>
            {
                //Check if the user can create a container
                var subscriptionFeatures = await subscriptionClient.GetSubscriptionFeaturesAsync(container.UserId);

                var numberOfUserContainers = await containersService
                    .GetAllContainerByUserIdAsync(container.UserId);

                var numberOfRemainingContainers =
                    subscriptionFeatures.Subscription.SubscriptionFeatures.NumberOfContainers -
                    numberOfUserContainers.Count;

                if (numberOfRemainingContainers is 0)
                {
                    return Results.Problem(statusCode: 403);
                }

                var result = await containersService.CreateContainerAsync(container);

                if (result is not { } containerResult) return Results.BadRequest();
                

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
                
                await notificationServiceBus.SendMessage(new NotificationModel
                {
                    GlobalUserId = result.UserId,
                    Heading = "A new container is created",
                    Message = $"You have created {result.Name} container"
                });

                return Results.Ok(containerResult);

            });

        /*
         * Route: /api/container/
         * DESC : Update a whole container in the database
         * Auth : Users
         */
        containersRouteGroup.MapPut("/",
            async (ContainersService containersService,
                IAzureServiceBus<HistoryMessage> serviceBus,
                IAzureServiceBus<NotificationMessage> notificationServiceBus,
                ContainerDto container) =>
            {
                if (string.IsNullOrEmpty(container.Id))
                    return Results.BadRequest();

                var result = await containersService
                    .UpdateContainerAsync(container);

                if (string.IsNullOrEmpty(result.Id)) return Results.NotFound();
                
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

                await notificationServiceBus.SendMessage(new NotificationModel
                {
                    GlobalUserId = result.UserId,
                    Heading = "A container is updated",
                    Message = $"You have deleted {result.Name} container"
                });

                return Results.Ok(); 
            });

        /*
         * Route: /api/container/id
         * DESC : Delete container in the database
         * Auth : Users
         */
        containersRouteGroup.MapDelete("/{id:length(24)}",
            async (ContainersService containersService,
                IAzureServiceBus<NotificationMessage> notificationServiceBus,
                string id) =>
            {
                var result = await containersService
                    .DeleteContainerAsync(id);

                if (result is null) Results.NotFound();

                var history = new HistoryModel
                {
                    Title = "A new container is deleted",
                    Action = ActionType.Delete,
                    UserId = result.UserId,
                    Service = SystemServices.Containers
                };

                await notificationServiceBus.SendMessage(new NotificationModel
                {
                    GlobalUserId = result.UserId,
                    Heading = "A container is Deleted",
                    Message = $"You have deleted {result.Name} container"
                });

                Results.Ok(result);
            });
        return app;
    }
}