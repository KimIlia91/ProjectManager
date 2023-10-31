using PM.Application.Common.Interfaces.IRepositories;
using Task = PM.Domain.Entities.Task;

namespace PM.Infrastructure.Persistence.Repositories;

public sealed class TaskRepository : BaseRepository<Task>, ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
