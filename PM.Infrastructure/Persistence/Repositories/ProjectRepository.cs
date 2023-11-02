using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Entities;

namespace PM.Infrastructure.Persistence.Repositories;

public sealed class ProjectRepository 
    : BaseRepository<Project>, IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(
        ApplicationDbContext context,
        IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }

    public async Task<List<GetProjectListResult>> ToProjectListResultAsync(
        IQueryable<Project> projectQuery, 
        CancellationToken cancellationToken)
    {
        return await projectQuery
            .ProjectToType<GetProjectListResult>(Mapper.Config)
            .ToListAsync(cancellationToken);
    }

    public async Task<GetProjectResult?> GetProjectByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Projects
            .Where(p => p.Id == id)
            .ProjectToType<GetProjectResult>(Mapper.Config)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
