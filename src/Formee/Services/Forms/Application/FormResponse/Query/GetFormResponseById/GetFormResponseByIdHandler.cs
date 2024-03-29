﻿namespace Application.FormResponse.Query.GetFormResponseById;
public class GetFormResponseByIdHandler : IRequestHandler<GetFormResponseByIdQuery, FormResponseEntity>
{
    private readonly IGenericRepository
        <FormResponseEntity> _formResponseRepository;

    public GetFormResponseByIdHandler(IGenericRepository
        <FormResponseEntity> formResponseRepository)
    {
        _formResponseRepository = formResponseRepository;
    }

    public async Task<FormResponseEntity> Handle(GetFormResponseByIdQuery request,
        CancellationToken cancellationToken)
    {
        if (request.FormResponseId is 0)
        {
            throw new BadRequestException(ErrorMessages.BadRequest);
        }

        var result = await _formResponseRepository
            .GetOneByIdAsync(f => f.Id == request.FormResponseId
                                  && f.IsDeleted != true,
                new[] { "FormResponseFields" });

        if (result is null)
            throw new NotFoundException(ErrorMessages.NotFound);

        return result;
    }
}
