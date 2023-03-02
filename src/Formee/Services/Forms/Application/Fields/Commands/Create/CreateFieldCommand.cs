namespace Application.Fields.Commands.Create;

public record CreateFieldCommand(FieldEntity Field) : IRequest<ResponseEntity>;
