using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PM.Domain.Common.Constants;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;

namespace PM.Infrastructure.Persistence.EntityConfigs;

public sealed class ProjectConfigurations : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(t => t.Id);

        builder.Property(p => p.Id)
            .HasColumnName("Id");

        builder.Property(p => p.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(EntityConstants.ProjectName);

        builder.Property(p => p.StartDate)
            .HasColumnName("StartDate")
            .IsRequired();

        builder.Property(p => p.EndDate)
            .HasColumnName("EndDate")
            .IsRequired();

        builder.Property(p => p.Priority)
            .HasColumnName("Priority")
            .HasConversion(new EnumToStringConverter<ProjectPriority>())
            .IsRequired();

        builder.HasOne<Employee>()
            .WithMany()
            .HasForeignKey(p => p.ManagerId)
            .IsRequired();

        builder.HasMany(p => p.Tasks)
            .WithOne()
            .HasForeignKey(p => p.ProjectId)
            .IsRequired();
        
        builder.HasOne(p => p.ExecutorCompany)
            .WithMany(c => c.ExecutedProjects)
            .IsRequired();

        builder.HasOne(p => p.CustomerCompany)
            .WithMany(c => c.CustomerProjects)
            .IsRequired();

        builder.HasMany(p => p.Employees)
            .WithMany(p => p.Projects);
    }
}
