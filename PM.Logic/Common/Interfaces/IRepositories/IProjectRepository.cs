using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Common.Interfaces.IRepositories;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<GetProjectResult?> GetProjectByIdAsync(
        int id, 
        CancellationToken cancellationToken);
}
