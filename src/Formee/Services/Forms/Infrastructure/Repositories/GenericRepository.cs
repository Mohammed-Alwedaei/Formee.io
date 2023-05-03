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
public class GenericRepository<TEntity> : IGenericRepository<TEntity>
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

        return await query.AsNoTracking().FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<List<TEntity>> GetAllByConditionAsync(
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

        return await query.AsNoTracking().ToListAsync();
    }

    /// <inheritdoc />
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var entry = await _context.Set<TEntity>().AddAsync(entity);

        return entry.Entity;
    }

    /// <inheritdoc />
    public async Task BulkCreateAsync(IReadOnlyList<TEntity> entities)
    {
        await _context
            .Set<TEntity>()
            .AddRangeAsync(entities);
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
        _context.Attach(entity);
        var entry = _context.Entry(entity);
        entry.State = EntityState.Modified;

        return entry.Entity;
    }
}