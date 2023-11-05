using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Entities;

namespace PM.Infrastructure.Persistence.Repositories;

/// <summary>
/// Project repository.
/// </summary>
public sealed class ProjectRepository
    : BaseRepository<Project>, IProjectRepository
{
    /// <summary>
    /// Constructor for the project repository.
    /// </summary>
    /// <param name="context">The application context.</param>
    /// <param name="mapper">Mapper for projects.</param>
    public ProjectRepository(
        ApplicationDbContext context,
        IMapper mapper) : base(context, mapper)
    {
    }

    /// <inheritdoc />
    public async Task<List<GetProjectListResult>> ToProjectListResultAsync(
        IQueryable<Project> projectQuery,
        CancellationToken cancellationToken)
    {
        return await projectQuery
            .ProjectToType<GetProjectListResult>(Mapper.Config)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<GetProjectResult?> GetProjectResultByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(p => p.Id == id)
            .ProjectToType<GetProjectResult>(Mapper.Config)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
