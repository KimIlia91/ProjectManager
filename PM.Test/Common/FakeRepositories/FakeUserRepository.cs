using Mapster;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Employee;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;

namespace PM.Test.Common.FakeRepositories;

public class FakeUserRepository : FakeBaseRepository<User>, IUserRepository
{
    public async Task<GetUserResult?> GetUserResultByIdAsync(
        int employeeId,
        CancellationToken cancellationToken)
    {
        return await Context.Users
            .ProjectToType<GetUserResult>(Mapper.Config)
            .FirstOrDefaultAsync(u => u.Id == employeeId);
    }

    public async Task<List<GetUserResult>> GetUserResultListAsync(
        CancellationToken cancellationToken)
    {
        return await Context.Users
            .ProjectToType<GetUserResult>(Mapper.Config)
            .ToListAsync(cancellationToken);
    }

    public Task<List<UserResult>> GetUserResultListByRoleAsync(
        string roleName,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserResult>> GetUsersResultListByProjectIdAsync(
        int projectId,
        CancellationToken cancellationToken)
    {
        return Context.Users
            .Where(u => u.Projects.Any(p => p.Id == projectId))
            .ProjectToType<UserResult>(Mapper.Config)
            .ToListAsync(cancellationToken);
    }
}
