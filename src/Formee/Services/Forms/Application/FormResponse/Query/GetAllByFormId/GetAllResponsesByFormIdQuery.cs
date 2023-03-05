namespace Application.FormResponse.Query.GetAllByFormId;

public record GetAllResponsesByFormIdQuery(int Id) : IRequest<ResponseEntity>;
