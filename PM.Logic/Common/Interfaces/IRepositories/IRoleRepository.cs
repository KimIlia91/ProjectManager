using PM.Application.Features.RoleContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Common.Interfaces.IRepositories;

/// <summary>
/// Role repository interface, inherits from the base repository.
/// </summary>
public interface IRoleRepository : IBaseRepository<Role>
{
    /// <summary>
    /// Gets a list of role results.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of role results.</returns>
    Task<List<GetRoleListResult>> GetRoleListResultAsync(
        CancellationToken cancellationToken);
}
