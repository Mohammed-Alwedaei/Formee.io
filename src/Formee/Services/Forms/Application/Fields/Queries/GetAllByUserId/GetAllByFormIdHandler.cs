namespace Application.Fields.Queries.GetAllByUserId;

public class GetAllByFormIdHandler : IRequestHandler
    <GetAllByFormIdQuery, ResponseEntity>
{
    private readonly IGenericRepository<FieldEntity> _genericRepository;

    public GetAllByFormIdHandler(IGenericRepository
        <FieldEntity> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<ResponseEntity> Handle(GetAllByFormIdQuery request, 
        CancellationToken cancellationToken)
    {
        if (request.FormId is 0)
            return new ResponseEntity
            {
                Error = "Invalid UserId input"
            };

        var result = await _genericRepository
            .GetAllByConditionAsync(f => f.FormId == request.FormId);

        if (!result.Any()) return new ResponseEntity
        {
            Error = $"The requested entities for form id of {request.FormId} " +
                    "are not found",
        };

        return new ResponseEntity
        {
            IsSuccessRequest = true,
            Results = result
        };
    }
}
