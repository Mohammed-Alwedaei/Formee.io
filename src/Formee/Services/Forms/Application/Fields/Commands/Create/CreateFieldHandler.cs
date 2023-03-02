namespace Application.Fields.Commands.Create;

public class CreateFieldHandler : IRequestHandler
    <CreateFieldCommand, ResponseEntity>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<FieldEntity> _genericRepository;

    public CreateFieldHandler(IUnitOfWork unitOfWork, IGenericRepository<FieldEntity> genericRepository)
    {
        _unitOfWork = unitOfWork;
        _genericRepository = genericRepository;
    }

    public async Task<ResponseEntity> Handle(CreateFieldCommand request, 
        CancellationToken cancellationToken)
    {
        var validator = new CreateFieldValidator();

        var validationResult = await validator.ValidateAsync(request.Field, 
            cancellationToken);

        if (!validationResult.IsValid)
            return new ResponseEntity
            {
                Error = validationResult.Errors.FirstOrDefault()?.ToString(),
            };

        var result = await _genericRepository
            .CreateAsync(request.Field);

        await _unitOfWork.SaveChangesAsync();

        return new ResponseEntity
        {
            IsSuccessRequest = true,
            Results = result
        };
    }
}
