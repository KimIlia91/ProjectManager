using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Common.Interfaces.IRepositories;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<List<GetProjectListResult>> ToProjectListResultAsync(
        IQueryable<Project> projectQuery,
        CancellationToken cancellationToken);

    Task<GetProjectResult?> GetProjectByIdAsync(
        int id, 
        CancellationToken cancellationToken);
}
