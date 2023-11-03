using ErrorOr;
using PM.Application.Features.AuthContext.Dtos;
using PM.Domain.Entities;
using Task = System.Threading.Tasks.Task;

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

    Task<ErrorOr<LoginResult>> LoginAsync(
        string email,
        string password);

    Task LogOutAsync(int userId);

    Task<ErrorOr<LoginResult>> RefreshAccessTokenAsync(string refreshToken);
}
