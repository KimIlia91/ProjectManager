using ErrorOr;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PM.Domain.Entities;
using System.Text;

namespace PM.Infrastructure.Auth.Services;

public sealed class RefreshTokenService : DataProtectorTokenProvider<User>
{
    private readonly UserManager<User> _userManager;

    public RefreshTokenService(
        IDataProtectionProvider dataProtectionProvider, 
        IOptions<DataProtectionTokenProviderOptions> options, 
        ILogger<DataProtectorTokenProvider<User>> logger,
        UserManager<User> userManager) : base(dataProtectionProvider, options, logger)
    {
        _userManager = userManager;
    }

    public Task<string> GenerateAsync(User user)
    {
        return GenerateAsync("RefreshToken", _userManager, user);
    }

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
                    // invalid purpose
                    return Error.Unauthorized();
                }

                var stamp = reader.ReadString();
                if (reader.PeekChar() != -1)
                {
                    // ValidateAsync failed: unexpected end of input.
                    return Error.Unauthorized();
                }

                if (_userManager.SupportsUserSecurityStamp)
                {
                    var isEqualsSecurityStamp = stamp == await _userManager.GetSecurityStampAsync(user);
                    //return new RefreshTokenValidationResult<TUser>(isEqualsSecurityStamp, user);
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
            // Logger.LogWarning("ValidateAsync failed: unhandled exception was thrown.");
            Logger.LogError(exception, "ValidateAsync failed: unhandled exception was thrown");
        }

        return Error.Unauthorized();
    }
}
