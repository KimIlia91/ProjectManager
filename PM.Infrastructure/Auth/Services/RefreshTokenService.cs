using ErrorOr;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PM.Domain.Entities;
using System.Text;

namespace PM.Infrastructure.Auth.Services;

/// <summary>
/// Service for handling the generation and validation of refresh tokens used for authentication.
/// </summary>
public sealed class RefreshTokenService : DataProtectorTokenProvider<User>
{
    private readonly UserManager<User> _userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenService"/> class.
    /// </summary>
    /// <param name="dataProtectionProvider">The data protection provider to use for token protection.</param>
    /// <param name="options">The options for the token provider.</param>
    /// <param name="logger">The logger for capturing log events.</param>
    /// <param name="userManager">The user manager used to retrieve user information.</param>
    public RefreshTokenService(
        IDataProtectionProvider dataProtectionProvider, 
        IOptions<DataProtectionTokenProviderOptions> options, 
        ILogger<DataProtectorTokenProvider<User>> logger,
        UserManager<User> userManager) : base(dataProtectionProvider, options, logger)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Generates a refresh token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the refresh token is generated.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is the generated refresh token as a string.</returns>
    public Task<string> GenerateAsync(User user)
    {
        return GenerateAsync("RefreshToken", _userManager, user);
    }

    /// <summary>
    /// Validates a refresh token.
    /// </summary>
    /// <param name="token">The refresh token to validate.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is 
    /// an <see cref="ErrorOr{T}"/> containing the validated user or an error 
    /// if validation fails.</returns>
    public async Task<ErrorOr<User>> ValidateAsync(string token)
    {
        try
        {
            var unprotectedData = Protector.Unprotect(Convert.FromBase64String(token));
            var ms = new MemoryStream(unprotectedData);
            using var reader = new BinaryReader(ms, new UTF8Encoding(false, true), true);
            {
                var creationTime = new DateTimeOffset(reader.ReadInt64(), TimeSpan.Zero);
                var expirationTime = creationTime + Options.TokenLifespan;
                if (expirationTime < DateTimeOffset.UtcNow)
                {
                    return Error.Unauthorized();
                }

                var userId = reader.ReadString();
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Error.Unauthorized();
                }

                var purpose = reader.ReadString();
                if (!string.Equals(purpose, "RefreshToken"))
                {
                    return Error.Unauthorized();
                }

                var stamp = reader.ReadString();
                if (reader.PeekChar() != -1)
                {
                    return Error.Unauthorized();
                }

                if (_userManager.SupportsUserSecurityStamp)
                {
                    var isEqualsSecurityStamp = stamp == await _userManager.GetSecurityStampAsync(user);

                    if (!isEqualsSecurityStamp)
                        return Error.Unauthorized();

                    return user;
                }


                var stampIsEmpty = stamp == "";

                if (!stampIsEmpty)
                    return Error.Unauthorized();

                return user;
            }
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "ValidateAsync failed: unhandled exception was thrown");
        }

        return Error.Unauthorized();
    }
}
