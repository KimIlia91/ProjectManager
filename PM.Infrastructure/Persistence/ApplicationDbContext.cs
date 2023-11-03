using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PM.Domain.Entities;
using Task = PM.Domain.Entities.Task;

namespace PM.Infrastructure.Persistence;

public class ApplicationDbContext 
    : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, 
        IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public override DbSet<Role> Roles { get; set; }

    public override DbSet<UserRole> UserRoles { get; set; }

    public DbSet<User> Employees { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<Task> Tasks { get; set; }

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
