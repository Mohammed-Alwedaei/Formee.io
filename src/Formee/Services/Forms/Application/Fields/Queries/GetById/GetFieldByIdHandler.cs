namespace Application.Fields.Queries.GetById;

/// <summary>
/// Get field by primary key (Id)
/// </summary>
public class GetFieldByIdHandler : IRequestHandler
    <GetFieldByIdQuery, ResponseEntity>
{
    private readonly IGenericRepository<FieldEntity> _genericRepository;

    public GetFieldByIdHandler(IGenericRepository<FieldEntity> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResponseEntity> Handle(GetFieldByIdQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Id == 0)
        {
            throw new BadRequestException(ErrorMessages.BadRequest);
        }    

        var result = await _genericRepository
            .GetOneByIdAsync(f => f.Id == request.Id);

        if (result is null)

        {
            throw new NotFoundException(ErrorMessages.NotFound);
        }

        return new ResponseEntity
        {
            Results = result
        };
    }
}
