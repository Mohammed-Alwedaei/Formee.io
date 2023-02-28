using Domain.Abstractions;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

/// <summary>
/// 
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
