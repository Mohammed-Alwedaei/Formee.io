﻿namespace Application.FormResponse.Query.GetAllByFormId;

public class GetAllResponsesByFormIdHandler 
    : IRequestHandler<GetAllResponsesByFormIdQuery, List<FormResponseEntity>>
{
    private readonly IGenericRepository<FormResponseEntity>
        _formResponseRepository;

    public GetAllResponsesByFormIdHandler(IGenericRepository
        <FormResponseEntity> formResponseRepository)
    {
        _formResponseRepository = formResponseRepository;
    }

    public async Task<List<FormResponseEntity>> Handle(GetAllResponsesByFormIdQuery request,
        CancellationToken cancellationToken)
    {
        if (request.FormId is 0)
        {
            throw new BadRequestException(ErrorMessages.BadRequest);
        }

        var result = await _formResponseRepository
            .GetAllByConditionAsync(
                x => x.FormId == request.FormId,
                new[]
                {
                    "FormResponseFields"
                });

        if (result.Any() is false)
        {
            throw new NotFoundException(ErrorMessages.NotFound);
        }

        return result;
    }
}
