using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.DbContexts;

namespace Infrastructure.Repositories;

public class EmailsManager : IEmailsManager
{
    private readonly ApplicationDbContext _context;

    public EmailsManager(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EmailEntity> GetOneByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<EmailEntity>> GetAllByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<EmailEntity> CreateAndSendAsync(EmailEntity entity)
    {
        throw new NotImplementedException();
    }
}
