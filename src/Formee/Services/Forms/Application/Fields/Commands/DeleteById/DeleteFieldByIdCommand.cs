namespace Application.Fields.Commands.DeleteById;

public record DeleteFieldByIdCommand(int Id) : IRequest<ResponseEntity>;
