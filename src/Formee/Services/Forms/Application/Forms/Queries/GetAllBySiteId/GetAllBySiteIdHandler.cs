namespace Application.Forms.Queries.GetAllBySiteId;

public class GetAllBySiteIdHandler : IRequestHandler
    <GetAllBySiteIdQuery, List<FormEntity>>
{
    private readonly IGenericRepository<FormEntity> _genericRepository;

    public GetAllBySiteIdHandler(IGenericRepository<FormEntity> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<List<FormEntity>> Handle(GetAllBySiteIdQuery request,
        CancellationToken cancellationToken)
    {
        var formsFromDb = await _genericRepository
            .GetAllByConditionAsync(
                f => f.SiteId == request.SiteId, new[]
            {
                "Fields",
                "Details"
            });

        if (!formsFromDb.Any())
        {
            throw new NotFoundException(ErrorMessages.NotFound);
        }

        return formsFromDb;
    }
}
