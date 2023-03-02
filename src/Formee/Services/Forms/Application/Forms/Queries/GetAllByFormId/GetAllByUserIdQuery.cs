namespace Application.Forms.Queries.GetAllByFormId;

public record GetAllByUserIdQuery(Guid UserId) : IRequest<ResponseEntity>;
