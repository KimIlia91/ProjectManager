using Microsoft.AspNetCore.Identity;

namespace PM.Infrastructure.Auth.Settings;

/// <summary>
/// Represents options for configuring the Refresh Token provider.
/// </summary>
public class RefreshTokenProviderOptions : DataProtectionTokenProviderOptions
{
    /// <summary>
    /// Gets or sets the token lifetime in minutes.
    /// </summary>
    /// <remarks>
    /// The token lifetime represents the duration for which the refresh token is valid.
    /// </remarks>
    public double TokenLifeTimeInMinutes
    {
        get => TokenLifespan.TotalMinutes;
        set => TokenLifespan = TimeSpan.FromMinutes(value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenProviderOptions"/> class.
    /// </summary>
    public RefreshTokenProviderOptions()
    {
        Name = string.Empty;
    }
}
