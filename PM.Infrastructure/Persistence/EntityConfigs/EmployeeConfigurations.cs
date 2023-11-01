using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Domain.Common.Constants;
using PM.Domain.Entities;

namespace PM.Infrastructure.Persistence.EntityConfigs;

public sealed class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("Id");

        builder.Property(e => e.FirstName)
            .HasColumnName("FirstName")
            .IsRequired()
            .HasMaxLength(EntityConstants.FirstName);

        builder.Property(e => e.LastName)
            .HasColumnName("LastName")
            .IsRequired()
            .HasMaxLength(EntityConstants.LastName);

        builder.Property(e => e.MiddleName)
            .HasColumnName("MiddleName")
            .IsRequired(false)
            .HasMaxLength(EntityConstants.MiddelName);

        builder.HasMany(e => e.AuthorTasks)
            .WithOne(t => t.Author)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.ExecutorTasks)
            .WithOne(t => t.Executor)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.ManageProjects)
            .WithOne(t => t.Manager)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.Projects)
            .WithMany(t => t.Employees);
    }
}
