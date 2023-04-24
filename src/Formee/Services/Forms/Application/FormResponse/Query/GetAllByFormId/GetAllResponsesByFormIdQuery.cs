namespace Application.FormResponse.Query.GetAllByFormId;

public record GetAllResponsesByFormIdQuery(int FormId) 
    : IRequest<List<FormResponseEntity>>;
