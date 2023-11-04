using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.IRepositories;
using System.Linq.Expressions;

namespace PM.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents a generic base repository for data access operations.
/// </summary>
/// <typeparam name="TEntity">The type of entity for which the repository is used.</typeparam>
public class BaseRepository<TEntity>
    : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> for the specified entity type.
    /// </summary>
    protected DbSet<TEntity> DbSet { get; }

    /// <summary>
    /// Gets the <see cref="IMapper"/> instance for mapping between entity types.
    /// </summary>
    protected IMapper Mapper { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.
    /// </summary>
    /// <param name="context">The application's database context.</param>
    /// <param name="mapper">The mapper used for mapping between entity types.</param>
    public BaseRepository(
        ApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        DbSet = _context.Set<TEntity>();
        Mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<List<TResult>> ToListResultAsync<TResult>(
        IQueryable<TEntity> projectQuery,
        CancellationToken cancellationToken)
    {
        return await projectQuery
            .ProjectToType<TResult>(Mapper.Config)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public IQueryable<TEntity> GetQuiery(
      bool asNoTracking = false)
    {
        if (asNoTracking)
            return DbSet.AsNoTracking();

        return DbSet;
    }

    /// <inheritdoc />
    public async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetOrDeafaultAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken)
    {
        return await DbSet.FirstOrDefaultAsync(filter, cancellationToken);
    }

    /// <inheritdoc />
    public async Task RemoveAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        DbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
