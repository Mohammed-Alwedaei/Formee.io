using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AgentRepository : IAgentRepository
{
    private readonly ApplicationDbContext _context;

    public AgentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AgentEntity?> GetOneByIdAsync(int id)
    {
        return await _context.Agents
            .FirstOrDefaultAsync(a => a!.Id == id);
    }

    public async Task<List<AgentEntity?>> GetAllByContainerIdAsync
        (string containerId)
    {
        return await _context.Agents
            .Where(a => a.ContainerId == containerId)
            .ToListAsync();
    }

    public async Task<AgentEntity?> CreateOneAsync(AgentEntity agent)
    {
        var createdAgent = await _context.Agents
            .AddAsync(agent);

        await _context.SaveChangesAsync();

        return createdAgent.Entity;
    }

    public async Task<AgentEntity?> DeleteOneAsync(int id)
    {
        var toDeleteAgent = await _context.Agents.
            FirstOrDefaultAsync(a => a!.Id == id);

        if (toDeleteAgent is null)
        {
            return new AgentEntity();
        }

        var deletedAgent = _context.Agents
            .Remove(toDeleteAgent);

        await _context.SaveChangesAsync();

        return deletedAgent.Entity;
    }
}
