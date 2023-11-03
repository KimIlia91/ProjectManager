using PM.Application.Common.Models.Employee;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Common.Interfaces.IRepositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<GetUserResult?> GetEmployeeByIdAsync(
        int employeeId,
        CancellationToken cancellationToken);

    Task<List<GetUserResult>> GetEmployeesAsync(
        CancellationToken cancellationToken);

    Task<List<UserResult>> GetUserResultListByRoleAsync(
        string roleName,
        CancellationToken cancellationToken);
}
