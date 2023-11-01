using PM.Application.Common.Interfaces.IRepositories;
using PM.Domain.Entities;

namespace PM.Infrastructure.Persistence.Repositories;

public sealed class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}
