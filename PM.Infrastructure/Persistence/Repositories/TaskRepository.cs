using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Dtos;
using Task = PM.Domain.Entities.Task;

namespace PM.Infrastructure.Persistence.Repositories;

public sealed class TaskRepository 
    : BaseRepository<Task>, ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(
        ApplicationDbContext context,
        IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }

    public async Task<GetTaskResult?> GetTaskByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .Where(t => t.Id == id)
            .ProjectToType<GetTaskResult>(Mapper.Config)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Task>?> GetTaskByAuthorIdAsync(
       int id,
       CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .Include(t => t.Author)
            .Where(t => t.Author.Id == id)
            .ToListAsync(cancellationToken);
    }
}
