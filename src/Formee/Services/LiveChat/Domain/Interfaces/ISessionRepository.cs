namespace Domain.Interfaces;

public interface ISessionRepository
{
    Task<SessionEntity> GetOneByIdAsync(int id);

    Task<List<SessionEntity>> GetAllByContainerIdAsync(int containerId);

    Task<SessionEntity> UpdateOneAsync(AgentEntity agent);

    Task<SessionEntity> CreateAsync(SessionEntity session);
}
