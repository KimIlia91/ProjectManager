using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Application.Common.Identity.Models;

namespace PM.Infrastructure.Persistence.EntityConfigs;

public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("Id");

        builder.HasOne(u => u.Employee)
            .WithOne()
            .HasForeignKey<ApplicationUser>(u => u.EmployeeId);
    }
}
