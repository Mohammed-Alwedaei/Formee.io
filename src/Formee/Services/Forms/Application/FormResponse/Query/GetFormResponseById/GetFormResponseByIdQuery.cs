namespace Application.FormResponse.Query.GetFormResponseById;

public record GetFormResponseByIdQuery(int FormResponseId) 
    : IRequest<ResponseEntity>;