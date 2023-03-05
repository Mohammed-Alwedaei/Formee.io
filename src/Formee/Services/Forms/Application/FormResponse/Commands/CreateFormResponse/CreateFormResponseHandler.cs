using Application.FieldsWarehouse.Commands.CreateWarehouse;
using Application.FormResponse.Commands.DeleteFormResponse;

namespace Application.FormResponse.Commands.CreateFormResponse;

/// <summary>
/// Create a field warehouse and check if the warehouse has an active field
/// </summary>
public class CreateFormResponseHandler
    : IRequestHandler<CreateFormResponseCommand, ResponseEntity>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<FormResponseEntity>
        _warehouseRepository;
    private readonly IGenericRepository<FieldEntity>
        _fieldRepository;
    private readonly ILogger<CreateFormResponseHandler> _logger;

    public CreateFormResponseHandler(IUnitOfWork unitOfWork,
        ILogger<CreateFormResponseHandler> logger,
        IGenericRepository<FormResponseEntity> warehouseRepository,
        IGenericRepository<FieldEntity> fieldRepository)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
        _fieldRepository = fieldRepository;
    }
    public async Task<ResponseEntity> Handle(CreateFormResponseCommand request
        , CancellationToken cancellationToken)
    {
        var validator = new CreateFormResponseValidator();

        var validationResult = await validator.
            ValidateAsync(request.FormResponse, cancellationToken);

        if (validationResult.IsValid == false)
        {
            throw new BadRequestException(ErrorMessages.BadRequest);
        }

        var hasActiveFields = await _fieldRepository
            .GetAllByConditionAsync(f => f.FormId == request.FormResponse.FormId);

        if (hasActiveFields.Count 
            != request.FormResponse.FormResponseFields.Count)
        {
            throw new BadRequestException(ErrorMessages.BadRequest);
        }

        var result = await _warehouseRepository
            .CreateAsync(request.FormResponse);

        await _unitOfWork.SaveChangesAsync();

        return new ResponseEntity
        {
            Results = result,
        };
    }
}
