namespace Application.Forms.Queries.GetAllByUserId;

public record GetAllByUserIdQuery(Guid UserId) : IRequest<ResponseEntity>;
