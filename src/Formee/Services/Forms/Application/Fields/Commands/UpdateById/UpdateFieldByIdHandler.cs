using Microsoft.Extensions.Logging;

namespace Application.Fields.Commands.UpdateById;

/// <summary>
/// 
/// </summary>
public class UpdateFieldByIdHandler : 
    IRequestHandler<UpdateFieldByIdCommand, ResponseEntity>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<FieldEntity> _genericRepository;
    private readonly ILogger<UpdateFieldByIdHandler> _logger;

    public UpdateFieldByIdHandler(
        IUnitOfWork unitOfWork, 
        IGenericRepository<FieldEntity> genericRepository, 
        ILogger<UpdateFieldByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _genericRepository = genericRepository;
        _logger = logger;
    }

    public async Task<ResponseEntity> Handle(UpdateFieldByIdCommand request, 
        CancellationToken cancellationToken)
    {
        var validator = new UpdateFieldByIdValidator();

        var validationResult = await validator
            .ValidateAsync(request.Field, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException(ErrorMessages.BadRequest);
        }

        var fieldFromDb = await _genericRepository
            .GetOneByIdAsync(f => f.Id == request.Field.Id);

        if (fieldFromDb is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound);
        }

        var result = _genericRepository
            .UpdateAsync(request.Field);

        await _unitOfWork.SaveChangesAsync();

        return new ResponseEntity
        {
            Results = result
        };
    }
}
