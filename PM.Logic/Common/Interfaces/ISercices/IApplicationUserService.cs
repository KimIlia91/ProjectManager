using ErrorOr;
using PM.Application.Common.Identity.Models;
using PM.Application.Features.UserContext.Commands.Register;

namespace PM.Application.Common.Interfaces.ISercices;

public interface IApplicationUserService
{
    Task<ErrorOr<ApplicationUser>> RegistrAsync(RegisterCommand command);
}
