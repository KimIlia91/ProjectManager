using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Task;
using PM.Application.Features.TaskContext.Dtos;
using Task = PM.Domain.Entities.Task;

namespace PM.Test.Common.FakeRepositories;

public class FakeTaskRepositpry : FakeBaseRepository<Task>, ITaskRepository
{
    public async Task<List<Task>> GetTaskIncludeAuthorByAuthorIdAsync(
        int id, CancellationToken cancellationToken)
    {
        return await Context.Tasks
            .Include(t => t.Author)
            .Where(t => t.Id == id)
            .ToListAsync(cancellationToken);
    }

    public Task<Task?> GetTaskIncludeProjectAsync(
        int taskId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<GetTaskResult?> GetTaskResultByIdAsync(
        int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TaskResult?> GetTaskResultOfUserAsync(
        int taskId, int userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
