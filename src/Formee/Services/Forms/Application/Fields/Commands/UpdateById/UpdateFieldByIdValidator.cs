namespace Application.Fields.Commands.UpdateById;

public class UpdateFieldByIdValidator : AbstractValidator<FieldEntity>
{
    public UpdateFieldByIdValidator()
    {
        RuleFor(f => f.Id)
            .NotEmpty();

        RuleFor(f => f.FormId)
            .NotEmpty();

        RuleFor(f => f.Form)
            .Null();

        RuleFor(f => f.Name)
            .MinimumLength(5)
            .MaximumLength(15);

        RuleFor(f => f.DataType)
            .MinimumLength(2)
            .MaximumLength(15);
    }
}
