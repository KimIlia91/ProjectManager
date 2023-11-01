using PM.Application.Features.TaskContext.Dtos;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Interfaces.IRepositories;

public interface ITaskRepository : IBaseRepository<Task>
{
    Task<GetTaskResult?> GetTaskByIdAsync(
        int id, 
        CancellationToken cancellationToken);

    Task<List<Task>?> GetTaskByAuthorIdAsync(
       int id,
       CancellationToken cancellationToken);
}
