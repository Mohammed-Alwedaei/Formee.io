namespace Application.Fields.Commands.UpdateById;

public record UpdateFieldByIdCommand(FieldEntity Field) 
    : IRequest<ResponseEntity>;
