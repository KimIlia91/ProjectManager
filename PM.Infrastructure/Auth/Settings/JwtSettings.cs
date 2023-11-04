namespace PM.Infrastructure.Identity.Settings;

/// <summary>
/// Represents the settings for JSON Web Token (JWT) authentication.
/// </summary>
public sealed class JwtSettings
{
    /// <summary>
    /// Gets or sets the secret key used for JWT token generation and validation.
    /// </summary>
    public string Secret { get; init; } = null!;

    /// <summary>
    /// Gets or sets the expiration time in minutes for JWT tokens.
    /// </summary>
    public int ExpiryMinutes { get; init; }

    /// <summary>
    /// Gets or sets the issuer of the JWT tokens.
    /// </summary>
    public string Issuer { get; init; } = null!;

    /// <summary>
    /// Gets or sets the audience for which the JWT tokens are intended.
    /// </summary>
    public string Audience { get; init; } = null!;
}
