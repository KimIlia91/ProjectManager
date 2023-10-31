using Microsoft.AspNetCore.Identity;
using PM.Infrastructure.Identity.Models;

namespace PM.Infrastructure.Persistence.Repositories;

public class AppUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AppUserRepository(
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
}
