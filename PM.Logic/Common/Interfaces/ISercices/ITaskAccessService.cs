using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Interfaces.ISercices;

/// <summary>
/// Represents a service for checking access to tasks.
/// </summary>
public interface ITaskAccessService : IAccessService<Task>
{
    /// <summary>
    /// Checks if a user has access to a specific task.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="entity">The task entity to check access for.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>True if the user has access to the task, otherwise false.</returns>
    Task<bool> IsAccess(int userId, Task entity, CancellationToken cancellationToken);
}