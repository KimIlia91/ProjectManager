using Mapster;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Entities;

namespace PM.Test.Common.FakeRepositories;

public sealed class FakeProjectRepository
    : FakeBaseRepository<Project>, IProjectRepository
{
    public Task<GetProjectResult?> GetProjectOfUserAsync(
        int projectId, 
        int userId, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<GetProjectResult?> GetProjectResultByIdAsync(
        int id, 
        CancellationToken cancellationToken)
    {
        return await Context.Projects
            .Where(p => p.Id == id)
            .ProjectToType<GetProjectResult>(Mapper.Config)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<GetProjectListResult>> ToProjectListResultAsync(
        IQueryable<Project> projectQuery, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
