using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.IRepositories;
using System.Linq.Expressions;

namespace PM.Infrastructure.Persistence.Repositories;

public class BaseRepository<TEntity>
    : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;

    protected DbSet<TEntity> DbSet { get; }

    protected IMapper Mapper { get; }

    public BaseRepository(
        ApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        DbSet = _context.Set<TEntity>();
        Mapper = mapper;
    }

    public async Task<List<TResult>> ToListResultAsync<TResult>(
        IQueryable<TEntity> projectQuery,
        CancellationToken cancellationToken)
    {
        return await projectQuery
            .ProjectToType<TResult>(Mapper.Config)
            .ToListAsync(cancellationToken);
    }

    public IQueryable<TEntity> GetQuiery(
      bool asNoTracking = false)
    {
        if (asNoTracking)
            return DbSet.AsNoTracking();

        return DbSet;
    }

    public async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken)
    {
        IQueryable<TEntity> query = DbSet.AsNoTracking();
        query.Where(filter);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetOrDeafaultAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken)
    {
        return await DbSet.FirstOrDefaultAsync(filter, cancellationToken);
    }

    public async Task RemoveAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        DbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
