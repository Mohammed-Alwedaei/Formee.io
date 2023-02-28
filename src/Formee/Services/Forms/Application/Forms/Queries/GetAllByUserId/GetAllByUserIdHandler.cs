﻿namespace Application.Forms.Queries.GetAllByUserId;

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
            .GetAllByUserIdAsync(request.UserId, new[]
            {
                "Fields",
                "Details"
            });

        if (!formsFromDb.Any()) return new ResponseEntity
        {
            Error = $"The requested entities for user id of {request.UserId} are not found",
        };

        return new ResponseEntity
        {
            IsSuccessRequest = true,
            Results = formsFromDb
        };
    }
}
