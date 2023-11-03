using Microsoft.AspNetCore.Identity;

namespace PM.Infrastructure.Auth.Settings;

public class RefreshTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public double TokenLifeTimeInMinutes
    {
        get => TokenLifespan.TotalMinutes;
        set => TokenLifespan = TimeSpan.FromMinutes(value);
    }

    public RefreshTokenProviderOptions()
    {
        Name = string.Empty;
    }
}
