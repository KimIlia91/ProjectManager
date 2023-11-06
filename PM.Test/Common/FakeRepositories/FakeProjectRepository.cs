using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Entities;

namespace PM.Test.Common.FakeRepositories;

public sealed class FakeProjectRepository
    : FakeBaseRepository<Project>, IProjectRepository
{
    public Task<GetProjectResult?> GetProjectResultByIdAsync(
        int id, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<GetProjectListResult>> ToProjectListResultAsync(
        IQueryable<Project> projectQuery, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
