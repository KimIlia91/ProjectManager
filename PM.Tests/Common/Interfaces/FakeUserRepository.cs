using Mapster;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Employee;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;

namespace PM.Tests.Common.Interfaces
{
    internal class FakeUserRepository : FakeBaseRepository<User>, IUserRepository
    {
        public async Task<GetUserResult?> GetUserResultByIdAsync(
            int employeeId, 
            CancellationToken cancellationToken)
        {
            return await Context.Users
                .ProjectToType<GetUserResult>(Mapper.Config)
                .FirstOrDefaultAsync(u => u.Id == employeeId);
        }

        public Task<List<GetUserResult>> GetUserResultListAsync(
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
