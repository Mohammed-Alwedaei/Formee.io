using Domain.Constants;
using Links.BusinessLogic.Repositories.IRepository;
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

    public static WebApplication UseRedirectLinksEndpoints
        (this WebApplication app)
    {
        var redirectLinks = app
            .MapGroup("/api/links/redirects/");

        var logger = app.Logger;

        /*
         * ROUTE: /api/links/redirect/targetUrl
         * DESC : Redirect the user to a the original URL
         * AUTH : Anonymous
         */
        redirectLinks.MapGet("/{targetUrl}", async
            (ILinkRepository linkRepository, 
                ILinkHitRepository linkHitRepository,
                string targetUrl) =>
        {
            logger.LogInformation("GET: request to /api/links/all/{targetId} at {datetime}",
                targetUrl,
                DateTime.Now);

            if (string.IsNullOrEmpty(targetUrl))
            {
                throw new BadRequestException(ErrorMessages.BadRequest);
            }

            var result = await linkRepository
                .GetRedirectLinkAsync(targetUrl);

            if (result is null)
            {
                throw new NotFoundException(ErrorMessages.NotFound);
            }

            var hit = new LinkHitEntity
            {
                LinkId = result.Id
            };

            await linkHitRepository.CreateAsync(hit);

            return Results.Redirect(result.OriginalUrl, true);
        });

        /*
         * ROUTE: /api/links/redirect/targetUrl
         * DESC : Redirect the user to a the original URL
         * AUTH : Anonymous
         */
        redirectLinks.MapGet("all/{linkId:int}", async
        (ILinkHitRepository linkHitRepository, int linkId) =>
        {
            logger.LogInformation("GET: request to /api/links/redirects/all/{linkId} at {datetime}",
                linkId,
                DateTime.Now);

            if (linkId == 0)
            {
                throw new BadRequestException(ErrorMessages.BadRequest);
            }

            var result = await linkHitRepository
                .GetAllByLinkIdAsync(linkId);

            if (result is null)
            {
                throw new NotFoundException(ErrorMessages.NotFound);
            }

            return Results.Ok(result);
        });

        redirectLinks.MapGet("all/{containerId}/{startDate:DateTime}/{endDate:DateTime}", async
            (ILinkHitRepository linkHitRepository, 
                string containerId, 
                DateTime startDate, 
                DateTime endDate) =>
        {
            logger.LogInformation("GET: request to /api/links/all/{containerId}/{startDate:DateTime}/{endDate:DateTime} at {datetime}",
                containerId,
                DateTime.Now,
                startDate,
                endDate);

            if (string.IsNullOrEmpty(containerId))
            {
                throw new BadRequestException(ErrorMessages.BadRequest);
            }

            var result = await linkHitRepository
                .GetAllByContainerIdAsync(containerId, startDate, endDate);

            if (result is null)
            {
                throw new NotFoundException(ErrorMessages.NotFound);
            }

            return Results.Ok(result);
        });

        return app;
    }
}
