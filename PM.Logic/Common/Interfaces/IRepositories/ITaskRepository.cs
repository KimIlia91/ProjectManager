using PM.Application.Features.TaskContext.Dtos;
using AppTask = PM.Domain.Entities.Task;

namespace PM.Application.Common.Interfaces.IRepositories;

public interface ITaskRepository : IBaseRepository<AppTask>
{
    Task<GetTaskResult?> GetTaskByIdAsync(int id, CancellationToken cancellationToken);
}
