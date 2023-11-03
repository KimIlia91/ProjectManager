using Microsoft.AspNetCore.Identity;

namespace PM.Domain.Entities;

public sealed class Role : IdentityRole<int>
{
    private readonly List<UserRole> _employeeRoles = new();

    public IReadOnlyCollection<UserRole> EmployeeRoles => _employeeRoles.ToList();
}
