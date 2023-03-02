using System.Linq.Expressions;

namespace Domain.Abstractions;

/// <summary>
/// GenericRepository interface with the operations used in the API (Presentation) layer
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Get only one entity from the database using primary key (Id)
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="includes"></param>
    /// <returns>TEntity</returns>
    Task<TEntity> GetOneByIdAsync(Expression<Func<TEntity, bool>> filter, 
        string[]? includes = null);

    /// <summary>
    /// Get all entities that matches the condition
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="includes"></param>
    /// <returns>List of TEntity</returns>
    Task<List<TEntity>> GetAllByConditionAsync(
        Expression<Func<TEntity, bool>> filter, string[]? includes = null);

    /// <summary>
    /// Create an entity in the database table
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>TEntity</returns>
    Task<TEntity> CreateAsync(TEntity entity);

    /// <summary>
    /// Update an entity in the database table
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>TEntity</returns>
    TEntity UpdateAsync(TEntity entity);

    /// <summary>
    /// Delete entity from the database table
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    TEntity DeleteByIdAsync(TEntity entity);
}
