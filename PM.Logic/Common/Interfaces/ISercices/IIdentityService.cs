using ErrorOr;
using PM.Domain.Entities;

namespace PM.Application.Common.Interfaces.ISercices;

public interface IIdentityService
{
    Task<bool> IsEmailExistAsync(string email);

    Task<bool> IsRoleExistAsync(string email);

    Task<ErrorOr<User>> RegisterAsync(
        string password,
        string roleName,
        User employee);

    Task<ErrorOr<User>> UpdateAsync(
        User employee,
        CancellationToken cancellationToken);
}
