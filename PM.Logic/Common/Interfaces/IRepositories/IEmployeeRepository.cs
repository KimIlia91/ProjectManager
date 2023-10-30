using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Common.Interfaces.IRepositories;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    Task<GetEmployeeResult?> GetEmployeeByIdAsync(
        int employeeId,
        CancellationToken cancellationToken);
}
