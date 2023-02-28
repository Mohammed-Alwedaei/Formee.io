namespace Application.Forms.Commands.UpdateById;

/// <summary>
/// Update form by primary key (Id) request
/// </summary>
/// <param name="Form"></param>
public record UpdateFormByIdCommand(FormEntity Form) : IRequest<ResponseEntity>;
