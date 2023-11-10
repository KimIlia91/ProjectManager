using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Domain.Common.Constants;
using PM.Domain.Entities;

namespace PM.Infrastructure.Persistence.EntityConfigs;

public sealed class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(EntityConstants.FirstName);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(EntityConstants.LastName);

        builder.Property(e => e.MiddleName)
            .IsRequired(false)
            .HasMaxLength(EntityConstants.MiddelName);

        builder.HasMany(e => e.AuthorTasks)
            .WithOne(t => t.Author);

        builder.HasMany(e => e.ExecutorTasks)
            .WithOne(t => t.Executor);

        builder.HasMany(e => e.ManageProjects)
            .WithOne(t => t.Manager);

        builder.HasMany(e => e.Projects)
            .WithMany(t => t.Users);

        builder.HasMany(e => e.UserRoles)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);
    }
}
