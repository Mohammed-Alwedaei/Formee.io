using Domain.Abstractions;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

/// <summary>
/// GenericRepository class is the implementation of GenericRepository interface
/// GenericRepository contains all the operations used in the API (Presentation) layer
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TContext"></typeparam>
public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
    where TEntity : class
{
    private readonly ApplicationDbContext _context;

    internal DbSet<TEntity> DbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        this.DbSet = _context.Set<TEntity>();
    }

    /// <inheritdoc />
    public async Task<TEntity> GetOneByIdAsync(
        Expression<Func<TEntity, bool>> filter, string[]? includes = null)
    {
        IQueryable<TEntity> query = DbSet;

        query = query.Where(filter);

        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstAsync();
    }

    /// <inheritdoc />
    public async Task<List<TEntity>> GetAllByUserIdAsync(Guid userId, string[]? includes = null)
    {
        IQueryable<TEntity> query = DbSet;

        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var entry = await _context.Set<TEntity>().AddAsync(entity);

        return entry.Entity;
    }

    /// <inheritdoc />
    public TEntity UpdateAsync(TEntity entity)
    {
        var entry = _context.Set<TEntity>().Update(entity);

        return entry.Entity;
    }

    /// <inheritdoc />
    public TEntity DeleteByIdAsync(TEntity entity)
    {
        var entry = _context.Set<TEntity>().Remove(entity);

        return entry.Entity;
    }
}