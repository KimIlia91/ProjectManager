using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.RoleContext.Dtos;
using PM.Domain.Entities;

namespace PM.Infrastructure.Persistence.Repositories;

public sealed class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(
        ApplicationDbContext context, 
        IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<GetRoleListResult>> GetRoleListResultAsync(
        CancellationToken cancellationToken)
    {
        return await DbSet
            .ProjectToType<GetRoleListResult>(Mapper.Config)
            .ToListAsync(cancellationToken);
    }
}
