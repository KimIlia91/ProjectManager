using PM.Domain.Entities;

namespace PM.Application.Common.Interfaces.ISercices;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
