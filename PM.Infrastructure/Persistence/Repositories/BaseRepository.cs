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
    private readonly DbSet<TEntity> _dbSet;
    protected readonly IMapper _mapper;

    public BaseRepository(
        ApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
        _mapper = mapper;
    }

    public async Task<List<TResult>> ToListResultAsync<TResult>(
        IQueryable<TEntity> projectQuery,
        CancellationToken cancellationToken)
    {
        return await projectQuery
            .ProjectToType<TResult>(_mapper.Config)
            .ToListAsync(cancellationToken);
    }

    public IQueryable<TEntity> GetQuiery(
      bool asNoTracking = false)
    {
        if (asNoTracking)
            return _dbSet.AsNoTracking();

        return _dbSet;
    }

    public async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();
        query.Where(filter);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetOrDeafaultAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(filter, cancellationToken);
    }

    public async Task RemoveAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
