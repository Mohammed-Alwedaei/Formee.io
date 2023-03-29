namespace Domain.Interfaces;

public interface IAgentRepository
{
    Task<AgentEntity?> GetOneByIdAsync(int  id);

    Task<List<AgentEntity?>> GetAllByContainerIdAsync(string containerId);

    Task<AgentEntity?> CreateOneAsync(AgentEntity agent);

    Task<AgentEntity?> DeleteOneAsync(int id);
}
