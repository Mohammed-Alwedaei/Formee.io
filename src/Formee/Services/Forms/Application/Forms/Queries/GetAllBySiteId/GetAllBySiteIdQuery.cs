namespace Application.Forms.Queries.GetAllBySiteId;

public record GetAllBySiteIdQuery(int SiteId) : IRequest<ResponseEntity>;
