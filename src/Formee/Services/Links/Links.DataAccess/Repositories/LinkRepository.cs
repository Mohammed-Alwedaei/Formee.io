using System.Data;
using Dapper;
using Links.DataAccess.Contexts;
using Links.Utilities.Entities;
using Microsoft.AspNetCore.WebUtilities;

namespace Links.DataAccess.Repositories;

public class LinkRepository : ILinkRepository
{
    private readonly DbContext _db;

    public LinkRepository(DbContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task<LinkEntity> GetLinkByIdAsync(int id)
    {
        using var connection = _db.Connect();

        var linkFromDb = await connection.QueryAsync
            <LinkEntity>("sp_Link_GetById", new { Id = id },
                commandType: CommandType.StoredProcedure);

        return linkFromDb.FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<LinkEntity>> GetAllLinksByContainerIdAsync
        (string containerId)
    {
        using var connection = _db.Connect();

        var linksFromDb = await connection.QueryAsync
            <LinkEntity>("sp_Link_GetAllByContainerId",
                new { ContainerId = containerId },
                commandType: CommandType.StoredProcedure);

        return linksFromDb.ToList();
    }

    /// <inheritdoc />
    public async Task<LinkEntity> CreateLinkAsync(LinkEntity entity)
    {
        using var connection = _db.Connect();

        var linkToCreate = new
        {
            entity.ContainerId,
            entity.OriginalUrl,
            entity.IsDeleted,
            entity.CreatedDate
        };

        var result = await connection.QueryAsync
            <int>("sp_Link_Create",
                linkToCreate,
                commandType: CommandType.StoredProcedure);

        var linkToUpdateId = result.FirstOrDefault();

        var targetUrl = WebEncoders
            .Base64UrlEncode(BitConverter.GetBytes(linkToUpdateId));

        var updatedLink = await connection.QueryAsync
            <LinkEntity>("sp_Link_UpdateTargetUrlById",
                new { Id = linkToUpdateId, TargetUrl = targetUrl },
                commandType: CommandType.StoredProcedure);

        return updatedLink.FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<DeleteLinkDto> DeleteLinkAsync(int id)
    {
        using var connection = _db.Connect();

        var deletedLink = await connection.QueryAsync
            <DeleteLinkDto>("sp_Link_DeleteById",
                new { Id = id, DeletedDate = DateTime.Now },
                commandType: CommandType.StoredProcedure);

        return deletedLink.FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<List<DeleteLinkDto>> DeleteAllLinksByContainerIdAsync(
        string containerId)
    {
        using var connection = _db.Connect();

        var deletedLinks = await connection
            .QueryAsync<DeleteLinkDto>("sp_Link_DeleteAllByContainerId",
                new
                {
                    ContainerId = containerId,
                    DeletedDate = DateTime.Now
                },
                commandType: CommandType.StoredProcedure);

        return deletedLinks.ToList();
    }
}