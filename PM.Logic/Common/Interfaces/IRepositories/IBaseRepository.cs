using System.Linq.Expressions;

namespace PM.Application.Common.Interfaces.IRepositories;

/// <summary>
/// Represents a base repository interface for data access.
/// </summary>
/// <typeparam name="TEntity">The type of entities managed by the repository.</typeparam>
public interface IBaseRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Gets an entity from the repository that matches the specified filter condition.
    /// </summary>
    /// <param name="filter">The filter condition for selecting the entity.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe the cancellation request.</param>
    /// <returns>The matching entity or null if not found.</returns>
    Task<TEntity?> GetOrDeafaultAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken);

    /// <summary>
    /// Converts a query to a list of results of the specified type.
    /// </summary>
    /// <typeparam name="TResult">The type of results to be returned.</typeparam>
    /// <param name="projectQuery">The query to be converted to a list of results.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe the cancellation request.</param>
    /// <returns>A list of results of the specified type.</returns>
    Task<List<TResult>> ToListResultAsync<TResult>(
        IQueryable<TEntity> projectQuery,
        CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Adds an entity to the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to be added to the repository.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe the cancellation request.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RemoveAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Removes an entity from the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to be removed from the repository.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe the cancellation request.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Saves changes made to the repository asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe the cancellation request.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    IQueryable<TEntity> GetQuery(bool asNoTracking = false);
}
