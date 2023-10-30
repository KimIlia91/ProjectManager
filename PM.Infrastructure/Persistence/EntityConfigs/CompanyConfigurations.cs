using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Domain.Common.Constants;
using PM.Domain.Entities;

namespace PM.Infrastructure.Persistence.EntityConfigs;

public sealed class CompanyConfigurations : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("Id");

        builder.Property(c => c.Name)
            .HasColumnName("Name")
            .HasMaxLength(EntityConstants.CompanyName)
            .IsRequired();

        builder.HasMany(p => p.ExecutedProjects)
            .WithOne(c => c.ExecutorCompany)
            .IsRequired();

        builder.HasMany(p => p.CustomerProjects)
           .WithOne(c => c.CustomerCompany)
           .IsRequired();
    }
}
