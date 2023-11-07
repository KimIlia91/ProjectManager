using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Common.Interfaces.IRepositories;

/// <summary>
/// Project repository interface, inherits from the base repository.
/// </summary>
public interface IProjectRepository : IBaseRepository<Project>
{
    /// <summary>
    /// Converts query results to a list of project list objects.
    /// </summary>
    /// <param name="projectQuery">Project query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of project list objects.</returns>
    Task<List<GetProjectListResult>> ToProjectListResultAsync(
        IQueryable<Project> projectQuery,
        CancellationToken cancellationToken);

    /// <summary>
    /// Gets project information by its identifier.
    /// </summary>
    /// <param name="id">Project identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Project information.</returns>
    Task<GetProjectResult?> GetProjectResultByIdAsync(
        int id, 
        CancellationToken cancellationToken);

    /// <summary>
    /// Gets project information by its identifier.
    /// </summary>
    /// <param name="id">Project identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Project information.</returns>
    Task<GetProjectResult?> GetProjectOfUserByIdAsync(
        int projectId,
        int userId,
        CancellationToken cancellationToken);
}
