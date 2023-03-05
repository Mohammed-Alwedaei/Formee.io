namespace Application.FormResponse.Commands.DeleteFormResponse;

public record DeleteFormResponseCommand(int FormResponseId) 
    : IRequest<ResponseEntity>;
