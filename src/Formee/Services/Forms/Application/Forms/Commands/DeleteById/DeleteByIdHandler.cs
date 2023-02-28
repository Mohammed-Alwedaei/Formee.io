namespace Application.Forms.Commands.DeleteById;

/// <summary>
/// Delete form by primary key (Id) request handler
/// </summary>
public class DeleteByIdHandler : IRequestHandler<DeleteByIdCommand, 
    ResponseEntity>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<FormEntity> _genericRepository;

    public DeleteByIdHandler(IUnitOfWork unitOfWork, 
        IGenericRepository<FormEntity> genericRepository)
    {
        _unitOfWork = unitOfWork;
        _genericRepository = genericRepository;
    }

    /// <summary>
    /// Handle method implements IRequest class
    /// The method is called when even is sent to DeleteByIdCommand
    /// The method will check if the entity of the requested id is available or not
    /// if available it will delete it
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>ResponseEntity</returns>
    public async Task<ResponseEntity> Handle(DeleteByIdCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Id == 0)
            return new ResponseEntity
            {
                Error = "Entity id is not provided"
            };

        var entityFromDb = await _genericRepository
            .GetOneByIdAsync(x => x.Id == request.Id);

        if (entityFromDb.Id is 0 || entityFromDb.UserId == Guid.Empty)
            return new ResponseEntity
            {
                Error = "Could not find the requested entity"
            };

        var result = _genericRepository
            .DeleteByIdAsync(entityFromDb);

        if (result.Id is 0 || result.UserId == Guid.Empty)
            return new ResponseEntity
            {
                Error = "Could not delete the entity"
            };

        await _unitOfWork.SaveChangesAsync();

        return new ResponseEntity
        {
            IsSuccessRequest = true,
            Results = result
        };
    }
}
