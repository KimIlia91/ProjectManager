using PM.Application.Common.Models.Task;
using PM.Application.Features.TaskContext.Dtos;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Interfaces.IRepositories;

/// <summary>
/// Interface for managing task entities in the repository.
/// </summary>
public interface ITaskRepository : IBaseRepository<Task>
{
    /// <summary>
    /// Retrieves a task by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the task.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task result, or null if not found.</returns>
    Task<GetTaskResult?> GetTaskResultByIdAsync(
        int id, 
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a list of tasks authored by a user with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A list of tasks, or null if none are found.</returns>
    Task<List<Task>> GetTaskIncludeAuthorByAuthorIdAsync(
       int id,
       CancellationToken cancellationToken);

    /// <summary>
    /// Get task include project.
    /// </summary>
    /// <param name="taskId">The unique identifier of the task.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns></returns>
    Task<Task?> GetTaskIncludeProjectAsync(
       int taskId,
       CancellationToken cancellationToken);

    Task<TaskResult?> GetTaskResultOfUserAsync(
        int taskId,
        int userId,
        CancellationToken cancellationToken);
}
