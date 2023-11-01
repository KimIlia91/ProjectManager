using ErrorOr;
using PM.Domain.Entities;

namespace PM.Application.Common.Interfaces.ISercices;

public interface IIdentityService
{
    Task<bool> IsEmailExistAsync(string email);

    Task<bool> IsRoleExistAsync(string email);

    Task<ErrorOr<Employee>> RegisterAsync(
        string password,
        string roleName,
        Employee employee);

    Task<ErrorOr<Employee>> UpdateAsync(
        Employee employee,
        CancellationToken cancellationToken);
}
