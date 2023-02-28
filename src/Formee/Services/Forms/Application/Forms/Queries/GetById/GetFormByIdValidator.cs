using FluentValidation;

namespace Application.Forms.Queries.GetById;
public class GetFormByIdValidator : AbstractValidator<FormEntity>
{
    public GetFormByIdValidator()
    {
        RuleFor(f => f.Id)
            .NotEmpty();
    }
}
