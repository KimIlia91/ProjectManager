using PM.Application.Common.Models.Employee;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Common.Interfaces.IRepositories;

/// <summary>
/// Interface for managing user entities in the repository.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Retrieves user information by their unique identifier.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>User information, or null if not found.</returns>
    Task<GetUserResult?> GetUserResultByIdAsync(
        int employeeId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a list of user information asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A list of user information.</returns>
    Task<List<GetUserResult>> GetUserResultListAsync(
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a list of user information associated with a specific project.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A list of user information related to the project.</returns>
    Task<List<UserResult>> GetUsersResultListByProjectIdAsync(
        int projectId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a list of user information with a specific role.
    /// </summary>
    /// <param name="roleName">The name of the role to filter by.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A list of user information with the specified role.</returns>
    Task<List<UserResult>> GetUserResultListByRoleAsync(
        string roleName,
        CancellationToken cancellationToken);
}
