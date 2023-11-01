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
    private readonly IMapper _mapper;

    public TaskRepository(
        ApplicationDbContext context,
        IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetTaskResult?> GetTaskByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .Where(t => t.Id == id)
            .ProjectToType<GetTaskResult>(_mapper.Config)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
