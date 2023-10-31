using ErrorOr;
using PM.Application.Common.Identity.Models;
using PM.Domain.Entities;

namespace PM.Application.Common.Interfaces.ISercices;

public interface IApplicationUserService
{
    Task<ErrorOr<ApplicationUser>> RegistrAsync(
        string password,
        int roleId,
        Employee employee);
}
