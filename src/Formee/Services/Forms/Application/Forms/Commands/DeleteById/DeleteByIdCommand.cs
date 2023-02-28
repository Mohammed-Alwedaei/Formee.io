namespace Application.Forms.Commands.DeleteById;

/// <summary>
/// Delete form by form primary key request
/// </summary>
/// <param name="Id"></param>
public record DeleteByIdCommand(int Id) : IRequest<ResponseEntity>;
