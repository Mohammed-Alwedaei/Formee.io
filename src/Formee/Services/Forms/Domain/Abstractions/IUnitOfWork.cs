namespace Domain.Abstractions;

/// <summary>
/// UnitOfWork interface with the SaveChangesAsync method to save the changes after
/// calling [Create, Update, Delete] methods in the GenericRepository
/// </summary>
public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
