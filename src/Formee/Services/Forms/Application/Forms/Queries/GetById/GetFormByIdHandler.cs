namespace Application.Forms.Queries.GetById;

/// <summary>
/// Get form by id
/// </summary>
public class GetFormByIdHandler : IRequestHandler
    <GetFormByIdQuery, FormEntity>
{
    private readonly IGenericRepository<FormEntity> _genericRepository;

    public GetFormByIdHandler(IGenericRepository<FormEntity> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<FormEntity> Handle(GetFormByIdQuery request,
        CancellationToken cancellationToken)
    {
        var formFromDb = await _genericRepository
            .GetOneByIdAsync(c => c.Id == request.Id, new[]
            {
                "Fields",
                "Details"
            });

        if (formFromDb is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound);
        }


        return formFromDb;
    }
}
