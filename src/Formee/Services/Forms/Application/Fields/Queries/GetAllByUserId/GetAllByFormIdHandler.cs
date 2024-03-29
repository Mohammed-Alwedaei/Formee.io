﻿namespace Application.Fields.Queries.GetAllByUserId;

public class GetAllByFormIdHandler : IRequestHandler
    <GetAllByFormIdQuery, List<FieldEntity>>
{
    private readonly IGenericRepository<FieldEntity> _genericRepository;

    public GetAllByFormIdHandler(IGenericRepository
        <FieldEntity> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<List<FieldEntity>> Handle(GetAllByFormIdQuery request, 
        CancellationToken cancellationToken)
    {
        if (request.FormId is 0)
        {
            throw new BadRequestException(ErrorMessages.BadRequest);
        }

        var result = await _genericRepository
            .GetAllByConditionAsync(f => f.FormId == request.FormId);

        if (!result.Any()) 
        {
           throw new NotFoundException(ErrorMessages.NotFound);
        }

        return result;
    }
}
