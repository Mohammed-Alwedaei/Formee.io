namespace Application.Fields.Queries.GetById;

public record GetFieldByIdQuery(int Id) : IRequest<ResponseEntity>;
