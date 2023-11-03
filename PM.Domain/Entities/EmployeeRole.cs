using Microsoft.AspNetCore.Identity;

namespace PM.Domain.Entities;

public sealed class EmployeeRole : IdentityUserRole<int>
{
    public Employee Employee { get; private set; } = null!;

    public Role Role { get; private set; } = null!;
}
