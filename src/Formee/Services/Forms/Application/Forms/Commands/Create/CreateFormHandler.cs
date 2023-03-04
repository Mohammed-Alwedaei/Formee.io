namespace Application.Forms.Commands.Create;

/// <summary>
/// Create new form handler
/// </summary>
public class CreateFormHandler : IRequestHandler<CreateFormCommand, ResponseEntity>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<FormEntity> _genericRepository;

    public CreateFormHandler(IUnitOfWork unitOfWork,
        IGenericRepository<FormEntity> genericRepository)
    {
        _unitOfWork = unitOfWork;
        _genericRepository = genericRepository;
    }

    /// <summary>
    /// Handle method implements IRequest class
    /// The method is called when even is sent to CreateFormCommand
    /// This method will validate the form input and then save it into the database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ResponseEntity> Handle(CreateFormCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _genericRepository
            .CreateAsync(request.Form);

        await _unitOfWork.SaveChangesAsync();

        return new ResponseEntity
        {
            Results = result
        };
    }
}
