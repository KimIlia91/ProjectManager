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

        builder.Property(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(EntityConstants.ProjectName);

        builder.Property(p => p.CustomerCompany)
            .IsRequired()
            .HasMaxLength(EntityConstants.CompanyName);

        builder.Property(p => p.ExecutorCompany)
            .IsRequired()
            .HasMaxLength(EntityConstants.CompanyName);

        builder.Property(p => p.StartDate)
            .IsRequired();

        builder.Property(p => p.EndDate)
            .IsRequired();

        builder.Property(p => p.Priority)
            .HasConversion(new EnumToNumberConverter<Priority, int>())
            .IsRequired();

        builder.HasIndex(p => p.Priority);

        builder.HasOne(p => p.Manager)
            .WithMany(e => e.ManageProjects)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(p => p.Tasks)
            .WithOne(t => t.Project);

        builder.HasMany(p => p.Users)
            .WithMany(p => p.Projects);
    }
}