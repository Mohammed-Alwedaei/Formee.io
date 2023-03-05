namespace Application.FormResponse.Commands.DeleteFormResponse;

public class CreateFormResponseValidator
    : AbstractValidator<FormResponseEntity>
{
    public CreateFormResponseValidator()
    {
        RuleFor(f => f.Id)
            .Empty();

        RuleFor(w => w.FormId)
            .NotEmpty();

        RuleFor(f => f.FormResponseFields)
            .NotEmpty();

        RuleFor(w => w.FormResponseFields)
            .NotNull();
    }
}
