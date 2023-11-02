using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PM.Domain.Common.Constants;
using PM.Domain.Common.Enums;
using Task = PM.Domain.Entities.Task;
using TaskStatus = PM.Domain.Common.Enums.TaskStatus;

namespace PM.Infrastructure.Persistence.EntityConfigs;

public sealed class TaskConfigurations : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
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
            .HasMaxLength(EntityConstants.Comment)
            .IsRequired(false);

        builder.Property(x => x.Status)
            .HasColumnName("Status")
            .HasConversion(new EnumToStringConverter<TaskStatus>())
            .HasMaxLength(EntityConstants.EnumStatusLength)
            .IsRequired();

        builder.Property(x => x.Priority)
           .HasColumnName("Priority")
           .HasConversion(new EnumToNumberConverter<ProjectPriority, int>())
           .IsRequired();

        builder.HasOne(t => t.Project)
            .WithMany(p => p.Tasks);

        builder.HasOne(t => t.Executor)
            .WithMany(e => e.ExecutorTasks)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(t => t.Author)
            .WithMany(e => e.AuthorTasks)
            .IsRequired(false);
    }
}
