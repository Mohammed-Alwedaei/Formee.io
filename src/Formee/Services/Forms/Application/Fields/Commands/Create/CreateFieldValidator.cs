using FluentValidation;

namespace Application.Fields.Commands.Create;

public class CreateFieldValidator : AbstractValidator<FieldEntity>
{
    public CreateFieldValidator()
    {
        RuleFor(f => f.Id)
            .Empty();

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
