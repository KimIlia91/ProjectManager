using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PM.Application.Common.Interfaces.ISercices;
using PM.Domain.Entities;
using PM.Infrastructure.Identity.Settings;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PM.Infrastructure.Identity;

/// <summary>
/// Service for generating JWT (JSON Web Token) tokens used for user authentication.
/// </summary>
public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeService _dateTimeProvider;

    /// <summary>
    /// Initializes a new instance of the JwtTokenService class.
    /// </summary>
    /// <param name="dateTimeProvider">The service providing date and time information.</param>
    /// <param name="jwtOptions">Options for JWT token generation.</param>
    public JwtTokenService(
        IDateTimeService dateTimeProvider,
        IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }

    /// <inheritdoc />
    public string GenerateToken(User user, List<string> roleNames)
    {
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var roleClaims = CreateRoleClaimList(roleNames);

        claims.AddRange(roleClaims);

        var securiryToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            notBefore: _dateTimeProvider.UtcNow,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securiryToken);
    }

    private static List<Claim> CreateRoleClaimList(List<string> roleNames)
    {
        var roleClaims = new List<Claim>();

        foreach (var roleName in roleNames)
            roleClaims.Add(new Claim(ClaimTypes.Role, roleName));

        return roleClaims;
    }
}
