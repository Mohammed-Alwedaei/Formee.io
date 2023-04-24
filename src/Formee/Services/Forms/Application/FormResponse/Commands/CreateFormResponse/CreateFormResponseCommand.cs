namespace Application.FormResponse.Commands.CreateFormResponse;

public record CreateFormResponseCommand(FormResponseEntity FormResponse)
    : IRequest<FormResponseEntity>;
