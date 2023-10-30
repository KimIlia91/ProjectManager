using Microsoft.AspNetCore.Identity;
using PM.Domain.Entities;

namespace PM.Infrastructure.Models;

public sealed class ApplicationUser : IdentityUser<int>
{
    public int EmployeeId { get; set; }

    public Employee Employee { get; set; } = null!;
}
