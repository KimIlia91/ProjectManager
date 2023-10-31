using Microsoft.AspNetCore.Identity;
using PM.Domain.Entities;

namespace PM.Application.Common.Identity.Models;

public class ApplicationUser : IdentityUser<int>
{
    public int EmployeeId { get; set; }

    public Employee Employee { get; set; } = null!;
}
