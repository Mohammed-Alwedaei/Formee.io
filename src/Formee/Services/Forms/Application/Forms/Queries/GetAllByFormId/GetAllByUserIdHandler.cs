using Application.Forms.Queries.GetAllByFormId;

namespace Application.Forms.Queries.GetAllByUserId;

public class GetAllByUserIdHandler : IRequestHandler
    <GetAllByUserIdQuery, ResponseEntity>
{
    private readonly IGenericRepository<FormEntity> _genericRepository;

    public GetAllByUserIdHandler(IGenericRepository<FormEntity> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<ResponseEntity> Handle(GetAllByUserIdQuery request, 
        CancellationToken cancellationToken)
    {
        var formsFromDb = await _genericRepository
            .GetAllByConditionAsync(
                f => f.UserId == request.UserId, new[]
            {
                "Fields",
                "Details"
            });

        if (!formsFromDb.Any())
        {
            throw new NotFoundException(ErrorMessages.NotFound);
        }

        return new ResponseEntity
        {
            Results = formsFromDb
        };
    }
}
