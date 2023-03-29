using Domain.Interfaces;
using Domain.Entities;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ApplicationDbContext  _context;

    public MessageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MessageEntity>> GetAllInSessionAsync(int sessionId)
    {
        return await _context.Messages
            .Where(a => a.SessionId == sessionId)
            .Order()
            .ToListAsync();
    }

    public async Task CreateMessageAsync(MessageEntity message)
    {
        var createdMessage = await _context.Messages.AddAsync(message);

        await _context.SaveChangesAsync();
    }
}
