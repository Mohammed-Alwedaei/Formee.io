namespace Application.Forms.Commands.UpdateById;

/// <summary>
/// Update form by primary key (Id) request handler
/// </summary>
public class UpdateFormByIdHandler : IRequestHandler<UpdateFormByIdCommand,
    FormEntity>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<FormEntity> _genericRepository;

    public UpdateFormByIdHandler(IUnitOfWork unitOfWork, 
        IGenericRepository<FormEntity> genericRepository)
    {
        _unitOfWork = unitOfWork;
        _genericRepository = genericRepository;
    }

    /// <summary>
    /// Handle method implements IRequest class
    /// The method is called when even is sent to UpdateFormByIdHandler
    /// The method will validate the input and then will update the entity
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>ResponseEntity</returns>
    public async Task<FormEntity> Handle(UpdateFormByIdCommand request, 
        CancellationToken cancellationToken)
    {
        var result = _genericRepository
            .UpdateAsync(request.Form);

        await _unitOfWork.SaveChangesAsync();

        return result;
    }
}
