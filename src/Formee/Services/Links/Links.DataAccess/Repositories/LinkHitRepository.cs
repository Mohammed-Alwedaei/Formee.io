using Dapper;
using Links.BusinessLogic.Contexts;
using Links.BusinessLogic.Repositories.IRepository;
using Links.Utilities.Entities;
using System.Data;

namespace Links.BusinessLogic.Repositories;

public class LinkHitRepository : ILinkHitRepository
{
    private readonly DbContext _db;

    public LinkHitRepository(DbContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task<List<LinkHitEntity>> GetAllByLinkIdAsync(int id)
    {
        using var connection = _db.Connect();

        var linkFromDb = await connection.QueryAsync
            <LinkHitEntity>("sp_Hit_GetAllById",
                new { LinkId = id },
                commandType: CommandType.StoredProcedure);

        return linkFromDb.ToList();
    }

    /// <inheritdoc />
    public async Task CreateAsync(LinkHitEntity hit)
    {
        using var connection = _db.Connect();

        await connection.ExecuteAsync("sp_Hit_Create",
                new
                {
                    hit.LinkId,
                    hit.CreatedDate
                },
                commandType: CommandType.StoredProcedure);
    }
}
