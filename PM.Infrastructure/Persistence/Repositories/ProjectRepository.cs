using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Entities;

namespace PM.Infrastructure.Persistence.Repositories;

public sealed class ProjectRepository 
    : BaseRepository<Project>, IProjectRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProjectRepository(
        ApplicationDbContext context,
        IMapper mapper) : base(context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetProjectResult?> GetProjectByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Projects
            .Where(p => p.Id == id)
            .ProjectToType<GetProjectResult>(_mapper.Config)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
