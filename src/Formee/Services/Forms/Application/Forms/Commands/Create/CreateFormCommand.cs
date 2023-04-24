namespace Application.Forms.Commands.Create;

/// <summary>
/// Create a new form request 
/// </summary>
/// <param name="Form"></param>
public record CreateFormCommand(FormEntity Form) : IRequest<FormEntity>;
