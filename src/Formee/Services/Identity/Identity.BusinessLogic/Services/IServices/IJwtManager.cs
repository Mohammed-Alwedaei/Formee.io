namespace Identity.BusinessLogic.Services.IServices;

public interface IJwtManager
{
    Task<string> GetAsync(string issuedFor);

    Task<string> CreateAsync(string issuedFor);

    Task<string> MarkAsExpiredAsync(Guid issuedFor);
}
