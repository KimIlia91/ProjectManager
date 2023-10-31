using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PM.Domain.Common.Constants;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;

namespace PM.Infrastructure.Persistence.EntityConfigs;

public sealed class TaskConfigurations : IEntityTypeConfiguration<Domain.Entities.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(t => t.Id);

        builder.Property(p => p.Id)
            .HasColumnName("Id");

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(EntityConstants.TaskName);

        builder.Property(x => x.Comment)
            .HasColumnName("Comment")
            .IsRequired(false);

        builder.Property(x => x.TaskStatus)
            .HasColumnName("TaskStatus")
            .HasConversion(new EnumToStringConverter<Domain.Common.Enums.TaskStatus>())
            .IsRequired();

        builder.Property(x => x.Priority)
           .HasColumnName("Priority")
           .HasConversion(new EnumToStringConverter<ProjectPriority>())
           .IsRequired();

        builder.HasOne<Project>()
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .IsRequired();
    }
}
