namespace PM.Infrastructure.Identity.Settings;

/// <summary>
/// Represents settings for token validation when processing JSON Web Tokens (JWT).
/// </summary>
public sealed class TokenValidationSettings
{
    /// <summary>
    /// Gets or sets a value indicating whether to validate the token issuer.
    /// </summary>
    public bool ValidateIssuer { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to validate the token audience.
    /// </summary>
    public bool ValidateAudience { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to validate the token lifetime.
    /// </summary>
    public bool ValidateLifetime { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to validate the issuer signing key.
    /// </summary>
    public bool ValidateIssuerSigningKey { get; set; }
}
