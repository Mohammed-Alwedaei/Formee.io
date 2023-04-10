using Analytics.Utilities.Dtos.Session;

namespace Analytics.BusinessLogic.Repositories.IRepository;

public interface ISessionRepository
{
    Task<SessionDto> GetAsync(string deviceId);

    Task<SessionDto> CreateAsync(SessionDto session);

    Task<SessionDto> UpdateAsync(SessionDto session);
}
