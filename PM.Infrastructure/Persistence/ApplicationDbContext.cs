using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PM.Domain.Entities;
using Task = PM.Domain.Entities.Task;

namespace PM.Infrastructure.Persistence;

public class ApplicationDbContext 
    : IdentityDbContext<Employee, ApplicationRole, int>
{
    public override DbSet<ApplicationRole> Roles { get; set; }

    public DbSet<Employee> Employees { get; set; }

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
