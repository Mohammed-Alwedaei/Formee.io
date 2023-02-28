namespace Application.Forms.Queries.GetById;

public record GetFormByIdQuery(int Id) : IRequest<ResponseEntity>;