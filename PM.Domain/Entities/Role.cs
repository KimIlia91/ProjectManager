using Microsoft.AspNetCore.Identity;

namespace PM.Domain.Entities;

public sealed class Role : IdentityRole<int>
{
    private readonly List<EmployeeRole> _employeeRoles = new();

    public IReadOnlyCollection<EmployeeRole> EmployeeRoles => _employeeRoles.ToList();
}
