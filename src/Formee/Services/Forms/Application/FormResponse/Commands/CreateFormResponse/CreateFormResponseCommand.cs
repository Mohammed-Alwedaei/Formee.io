namespace Application.FieldsWarehouse.Commands.CreateWarehouse;

public record CreateFormResponseCommand(FormResponseEntity FormResponse)
    : IRequest<ResponseEntity>;
