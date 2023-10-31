using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PM.Domain.Common.Constants;
using PM.Domain.Common.Enums;
using AppTask = PM.Domain.Entities.Task;

namespace PM.Infrastructure.Persistence.EntityConfigs;

public sealed class TaskConfigurations : IEntityTypeConfiguration<AppTask>
{
    public void Configure(EntityTypeBuilder<AppTask> builder)
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

        builder.HasOne(t => t.Project)
            .WithMany(p => p.Tasks);
    }
}
