using PM.Domain.Entities;

namespace PM.Application.Common.Interfaces.ISercices;

/// <summary>
/// Service for generating JWT (JSON Web Token) tokens used for user authentication.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <returns>The generated JWT token as a string.</returns>
    string GenerateToken(User user, List<string> roleNames);
}
