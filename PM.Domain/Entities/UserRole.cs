using Microsoft.AspNetCore.Identity;

namespace PM.Domain.Entities;

public sealed class UserRole : IdentityUserRole<int>
{
    public User Employee { get; private set; } = null!;

    public Role Role { get; private set; } = null!;
}
