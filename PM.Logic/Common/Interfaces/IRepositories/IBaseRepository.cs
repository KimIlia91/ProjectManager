using System.Linq.Expressions;

namespace PM.Application.Common.Interfaces.IRepositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken);

    Task<TEntity?> GetOrDeafaultAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken);

    Task<List<TResult>> ToListResultAsync<TResult>(
        IQueryable<TEntity> projectQuery,
        CancellationToken cancellationToken);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task RemoveAsync(TEntity entity, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);

    IQueryable<TEntity> GetQuiery(bool asNoTracking = false);
}
