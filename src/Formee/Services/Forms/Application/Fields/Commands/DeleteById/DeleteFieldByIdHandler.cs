namespace Application.Fields.Commands.DeleteById;

public class DeleteFieldByIdHandler 
    : IRequestHandler<DeleteFieldByIdCommand, ResponseEntity>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<FieldEntity> _genericRepository;
    private readonly ILogger<DeleteFieldByIdHandler> _logger;

    public DeleteFieldByIdHandler(
        IUnitOfWork unitOfWork,
        IGenericRepository<FieldEntity> genericRepository,
        ILogger<DeleteFieldByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _genericRepository = genericRepository;
        _logger = logger;
    }

    public async Task<ResponseEntity> Handle(DeleteFieldByIdCommand request,
        CancellationToken cancellationToken)
    {
        var fieldFromDb = await _genericRepository
            .GetOneByIdAsync(f => f.Id == request.Id);

        if (fieldFromDb == null)
        {
            _logger.LogWarning("could not find the entity of Id: {id}",
                request.Id);

            throw new NotFoundException("could not find the entity");
        }

        var toDeleteField = fieldFromDb;

        toDeleteField.IsDeleted = true;
        toDeleteField.IsModified = true;

        _genericRepository.DeleteByIdAsync(toDeleteField, x => x.IsDeleted);

        await _unitOfWork.SaveChangesAsync();

        return new ResponseEntity
        {
            Results = toDeleteField
        };
    }
}
