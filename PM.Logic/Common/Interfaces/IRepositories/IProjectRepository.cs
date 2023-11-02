using PM.Application.Features.ProjectContext.Dtos;
using PM.Application.Features.ProjectContext.Queries.GetProjectList;
using PM.Domain.Entities;

namespace PM.Application.Common.Interfaces.IRepositories;

public interface IProjectRepository : IBaseRepository<Project>
{
    IQueryable<Project> GetProjectQuiery(
        bool asNoTracking = false);

    Task<List<GetProjectListResult>> ToProjectListResultAsync(
        IQueryable<Project> projectQuery,
        CancellationToken cancellationToken);

    Task<GetProjectResult?> GetProjectByIdAsync(
        int id, 
        CancellationToken cancellationToken);
}
