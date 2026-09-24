using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Domain.ValueObjects.Role;
using Nexus.UserManagement.Infrastructure.Persistence.Constants;

namespace Nexus.UserManagement.Infrastructure.Persistence.Configurations
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(TableNames.Role);

            builder.HasKey(r => r.Id);

            builder.Property(c => c.Id)
              .HasColumnName("id")
              .HasConversion(roleId => roleId.Value, dbValue => RoleId.From(dbValue))
              .ValueGeneratedNever();

            builder.Property(r => r.Name)
                .HasColumnName("name")
                .HasConversion(
                    roleName => roleName.Value,
                    dbValue => RoleName.Create(dbValue))
                .UseCollation(PostgresConstants.COLLATION_NAME)
                .IsRequired();
        }
    }
}