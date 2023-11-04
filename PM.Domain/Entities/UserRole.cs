using Microsoft.AspNetCore.Identity;

namespace PM.Domain.Entities;

/// <summary>
/// Represents the association between a user and a role.
/// </summary>
public sealed class UserRole : IdentityUserRole<int>
{
    /// <summary>
    /// Gets the user associated with this role.
    /// </summary>
    public User User { get; private set; } = null!;

    /// <summary>
    /// Gets the role associated with this user.
    /// </summary>
    public Role Role { get; private set; } = null!;
}
