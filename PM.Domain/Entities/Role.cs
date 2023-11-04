using Microsoft.AspNetCore.Identity;

namespace PM.Domain.Entities;

/// <summary>
/// Represents a role in the authorization system.
/// </summary>
public sealed class Role : IdentityRole<int>
{
    private readonly List<UserRole> _userRoles = new();

    /// <summary>
    /// Gets user-role relationships associated with this role.
    /// </summary>
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.ToList();
}
