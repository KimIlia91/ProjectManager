using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PM.Domain.Entities;
using PM.Infrastructure.Models;

namespace PM.Infrastructure.Persistence;

public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public DbSet<ApplicationUser> Users { get; set; }

    public DbSet<ApplicationRole> Roles { get; set; }

    public DbSet<Project> Employees { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<Domain.Entities.Task> Tasks { get; set; }

    public DbSet<Project> Companies { get; set; }

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
