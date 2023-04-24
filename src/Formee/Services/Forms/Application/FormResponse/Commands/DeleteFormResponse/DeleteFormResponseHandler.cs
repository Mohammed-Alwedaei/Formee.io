namespace Application.FormResponse.Commands.DeleteFormResponse;

public class DeleteFormResponseHandler 
    : IRequestHandler<DeleteFormResponseCommand, FormResponseEntity>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<FormResponseEntity> 
        _formResponseRepository;

    public DeleteFormResponseHandler(IUnitOfWork unitOfWork, 
        IGenericRepository<FormResponseEntity> formResponseRepository)
    {
        _unitOfWork = unitOfWork;
        _formResponseRepository = formResponseRepository;
    }

    public async Task<FormResponseEntity> Handle(DeleteFormResponseCommand request,
        CancellationToken cancellationToken)
    {
        if (request.FormResponseId is 0)
        {
            throw new BadRequestException(ErrorMessages.BadRequest);
        }

        var formResponseFromDb = await _formResponseRepository
            .GetOneByIdAsync(x => x.Id == request.FormResponseId);

        if (formResponseFromDb is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound);
        }

        formResponseFromDb.IsDeleted = true;
        formResponseFromDb.IsModified = true;
        formResponseFromDb.LastModified = DateTime.Now;

        var result = _formResponseRepository
            .DeleteByIdAsync(formResponseFromDb);

        if (result is null)
        {
            throw new Exception(ErrorMessages.BadRequest);
        }

        await _unitOfWork.SaveChangesAsync();

        return result;
    }
}
