﻿using Domain.Constants;
using Links.DataAccess.Repositories;
using Links.Utilities.Entities;
using Links.Utilities.Exceptions;

namespace Links.API.Extensions;

public static class LinksEndpoints
{
    public static WebApplication UseLinksEndpoints
        (this WebApplication app)
    {
        var links = app.MapGroup("/api/links");

        var logger = app.Logger;

        /*
         * ROUTE: /api/links/id
         * DESC : Get a short link by id
         * AUTH : Users
         */
        links.MapGet("/{id:int}", async
            (ILinkRepository linkRepository, int id) =>
        {
            logger.LogInformation("GET: request to /api/links/{id} at {datetime}",
                id,
                DateTime.Now);

            if (id is 0)
            {
                throw new BadRequestException(ErrorMessages.BadRequest);
            }

            if (await linkRepository.GetLinkByIdAsync(id) is LinkEntity link)
            {
                return link;
            }

            throw new NotFoundException(ErrorMessages.NotFound);
        });

        /*
         * ROUTE: /api/links/containerId
         * DESC : Get a list of short links by containerId
         * AUTH : Users
         */
        links.MapGet("/all/{containerId}", async
            (ILinkRepository linkRepository, string containerId) =>
        {
            logger.LogInformation("GET: request to /api/links/{id} at {datetime}",
                containerId,
                DateTime.Now);

            if (string.IsNullOrEmpty(containerId) || containerId.Length > 24)
            {
                throw new BadRequestException(ErrorMessages.BadRequest);
            }

            var results = await linkRepository
                .GetAllLinksByContainerIdAsync(containerId);

            if (results.Count is not 0)
            {
                return results;
            }

            throw new NotFoundException(ErrorMessages.NotFound);
        });

        /*
         * ROUTE: /api/links
         * DESC : Create a short link
         * AUTH : Users
         */
        links.MapPost("/", async
            (ILinkRepository linkRepository, LinkEntity link) =>
        {
            logger.LogInformation("POST: request to /api/links at {datetime}",
                DateTime.Now);

            if (link is null)
            {
                throw new BadRequestException(ErrorMessages.BadRequest);
            }

            if (await linkRepository.CreateLinkAsync(link)
                is LinkEntity createdLink)
            {
                return createdLink;
            }

            throw new Exception(ErrorMessages.ServerError);
        });

        /*
         * ROUTE: /api/links/id
         * DESC : Delete a short link by id (Primary Key)
         * AUTH : Users
         */
        links.MapDelete("/{id:int}", async
            (ILinkRepository linkRepository, int id) =>
        {
            logger.LogInformation("GET: request to /api/links/{id} at {datetime}",
                id,
                DateTime.Now);

            if (id is 0)
            {
                throw new BadRequestException(ErrorMessages.BadRequest);
            }

            if (await linkRepository.DeleteLinkAsync(id)
                is DeleteLinkDto deletedLink)
            {
                return deletedLink;
            }

            throw new NotFoundException(ErrorMessages.NotFound);
        });

        /*
         * ROUTE: /api/links/containerId
         * DESC : Delete all short links by container id
         * AUTH : Users
         */
        links.MapDelete("/all/{containerId}", async
            (ILinkRepository linkRepository, string containerId) =>
        {
            logger.LogInformation("GET: request to /api/links/all/{id} at {datetime}",
                containerId,
                DateTime.Now);

            if (string.IsNullOrEmpty(containerId) || containerId.Length > 24)
            {
                throw new BadRequestException(ErrorMessages.BadRequest);
            }

            var results = await linkRepository
                .DeleteAllLinksByContainerIdAsync(containerId);

            if (results.Count is not 0)
            {
                return results;
            }

            throw new NotFoundException(ErrorMessages.NotFound);
        });

        return app;
    }
}
