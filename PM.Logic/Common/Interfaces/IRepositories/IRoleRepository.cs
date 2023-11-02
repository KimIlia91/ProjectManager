using PM.Application.Features.RoleContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Common.Interfaces.IRepositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<List<GetRoleListResult>> GetRoleListResultAsync(
        CancellationToken cancellationToken);
}
