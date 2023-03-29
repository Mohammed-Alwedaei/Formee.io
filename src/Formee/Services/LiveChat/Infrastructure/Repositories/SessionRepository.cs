using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.DbContexts;

namespace Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly ApplicationDbContext _context;

    public SessionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SessionEntity> GetOneByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<SessionEntity>> GetAllByContainerIdAsync(int containerId)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionEntity> UpdateOneAsync(AgentEntity agent)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionEntity> CreateAsync(SessionEntity session)
    {
        var result = await _context.Sessions
            .AddAsync(session);

        await _context.SaveChangesAsync();

        return result.Entity;
    }
}
