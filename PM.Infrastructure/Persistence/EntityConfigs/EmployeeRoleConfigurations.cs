using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Domain.Entities;

namespace PM.Infrastructure.Persistence.EntityConfigs;

public sealed class EmployeeRoleConfigurations : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("EmployeeRoles");

        builder.HasOne(er => er.Role)
            .WithMany(r => r.EmployeeRoles)
            .HasForeignKey(er => er.RoleId);

        builder.HasOne(er => er.Employee)
            .WithMany(r => r.EmployeeRoles)
            .HasForeignKey(er => er.UserId);
    }
}
