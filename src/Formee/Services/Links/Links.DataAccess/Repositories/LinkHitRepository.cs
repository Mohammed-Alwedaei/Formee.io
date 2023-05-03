using Dapper;
using Links.BusinessLogic.Contexts;
using Links.BusinessLogic.Repositories.IRepository;
using Links.Utilities.Entities;
using System.Data;
using Links.Utilities.Dtos;

namespace Links.BusinessLogic.Repositories;

public class LinkHitRepository : ILinkHitRepository
{
    private readonly AppContextService _db;

    public LinkHitRepository(AppContextService db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task<List<DateChartDto>> GetAllByLinkIdAsync(int id)
    {
        using var connection = _db.Connect();

        var linkHitsFromDb = await connection.QueryAsync
            <LinkHitEntity>("sp_Hit_GetAllById",
                new { LinkId = id },
                commandType: CommandType.StoredProcedure);

        return GenerateChartDto(linkHitsFromDb.ToList());
    }

    /// <inheritdoc />
    public async Task<List<DateChartDto>> GetAllByContainerIdAsync
        (string containerId, DateTime startDate, DateTime endDate)
    {
        using var connection = _db.Connect();

        var linkHitsFromDb = await connection.QueryAsync
            <LinkHitEntity>("sp_Hit_GetAllByContainerId",
                new
                {
                    containerId,
                    startDate,
                    endDate
                },
                commandType: CommandType.StoredProcedure);

        //Shape data to be used in the client

        return GenerateChartDto(linkHitsFromDb.ToList());
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

    private static List<DateChartDto> GenerateChartDto(List<LinkHitEntity> linkHits)
    {
        var linkHitsChartDto = new List<DateChartDto>();

        foreach (var hit in linkHits)
        {
            var isAvailableDate = linkHitsChartDto
                .FirstOrDefault(c => c.Date.Date == hit.CreatedDate.Date);
            if (isAvailableDate is not null)
            {
                isAvailableDate.Count++;
            }
            else
            {
                linkHitsChartDto.Add(new DateChartDto
                {
                    Id = hit.Id,
                    Date = hit.CreatedDate,
                    Count = 1
                });
            }
        }

        return linkHitsChartDto;
    }
}
