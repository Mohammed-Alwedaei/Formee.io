namespace Application.Fields.Queries.GetAllByUserId;

public record GetAllByFormIdQuery(int FormId) : IRequest<ResponseEntity>;
