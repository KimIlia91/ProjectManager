﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using PM.Application.Common.Interfaces.IRepositories;

namespace PM.Infrastructure.Persistence.Repositories;

public class BaseRepository<TEntity> 
    : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
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
