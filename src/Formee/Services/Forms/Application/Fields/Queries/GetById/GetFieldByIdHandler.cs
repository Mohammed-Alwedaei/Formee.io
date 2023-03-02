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
            return new ResponseEntity
            {
                Error = "Invalid id input"
            };

        var result = await _genericRepository
            .GetOneByIdAsync(f => f.Id == request.Id);

        if(result is null)
            return new ResponseEntity
            {
                Error = $"The requested entity of id {request.Id} is not found"
            };

        return new ResponseEntity
        {
            IsSuccessRequest = true,
            Results = result
        };
    }
}
