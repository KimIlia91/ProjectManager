using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PM.Domain.Entities;
using Task = PM.Domain.Entities.Task;

namespace PM.Infrastructure.Persistence;

/// <summary>
/// Represents the application's database context class.
/// </summary>
public class ApplicationDbContext 
    : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, 
        IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    /// <summary>
    /// Gets or sets the DbSet for user roles.
    /// </summary>
    public override DbSet<Role> Roles { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for user roles.
    /// </summary>
    public override DbSet<UserRole> UserRoles { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for users.
    /// </summary>
    public override DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for projects.
    /// </summary>
    public DbSet<Project> Projects { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for tasks.
    /// </summary>
    public DbSet<Task> Tasks { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used for configuring the context.</param>
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configures the model for the database context.
    /// </summary>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> to be used for model configuration.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
