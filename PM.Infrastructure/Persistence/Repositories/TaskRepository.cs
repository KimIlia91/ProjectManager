using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Task;
using PM.Application.Features.TaskContext.Dtos;
using Task = PM.Domain.Entities.Task;

namespace PM.Infrastructure.Persistence.Repositories;

/// <summary>
/// Task repository for managing task entities.
/// </summary>
public sealed class TaskRepository 
    : BaseRepository<Task>, ITaskRepository
{
    /// <summary>
    /// Constructs a new instance of the TaskRepository class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    public TaskRepository(
        ApplicationDbContext context,
        IMapper mapper) : base(context, mapper)
    {
    }

    /// <inheritdoc />
    public async Task<GetTaskResult?> GetTaskResultByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(t => t.Id == id)
            .ProjectToType<GetTaskResult>(Mapper.Config)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<List<Task>> GetTaskIncludeAuthorByAuthorIdAsync(
       int authorId,
       CancellationToken cancellationToken)
    {
        return await DbSet
            .Include(t => t.Author)
            .Where(t => t.Author.Id == authorId)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Task?> GetTaskIncludeProjectAsync(
       int taskId,
       CancellationToken cancellationToken)
    {
        return await DbSet
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
    }

    public async Task<TaskResult?> GetTaskResultOfUserAsync(
        int taskId, 
        int userId, 
        CancellationToken cancellationToken)
    {
        return await DbSet
            .ProjectToType<TaskResult>(Mapper.Config)
            .FirstOrDefaultAsync(t => t.Id == taskId &&
                                      (t.Author.Id == userId ||
                                      t.Executor.Id == userId), cancellationToken);
    }
}
