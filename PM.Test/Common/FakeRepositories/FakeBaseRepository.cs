using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Infrastructure.Persistence;
using PM.Test.Common.Data;
using System.Linq.Expressions;

namespace PM.Test.Common.FakeRepositories;

public class FakeBaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
{
    protected ApplicationDbContext Context { get; }

    protected IMapper Mapper { get; }

    public FakeBaseRepository()
    {
        Context = ApplicationDbContextFactory.Create();
        Mapper = new Mapper();
    }

    public async Task<List<TResult>> ToListResultAsync<TResult>(
        IQueryable<TEntity> projectQuery,
        CancellationToken cancellationToken)
    {
        return await projectQuery
            .ProjectToType<TResult>(Mapper.Config)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public IQueryable<TEntity> GetQuery(
      bool asNoTracking = false)
    {
        if (asNoTracking)
            return Context.Set<TEntity>().AsNoTracking();

        return Context.Set<TEntity>();
    }

    /// <inheritdoc />
    public async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetOrDeafaultAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(filter, cancellationToken);
    }

    /// <inheritdoc />
    public async Task RemoveAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await Context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        ApplicationDbContextFactory.Destroy(Context);
    }
}
