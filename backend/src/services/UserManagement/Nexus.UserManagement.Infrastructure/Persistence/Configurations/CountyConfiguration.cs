using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Domain.ValueObjects.Country;
using Nexus.UserManagement.Infrastructure.Persistence.Constants;

namespace Nexus.UserManagement.Infrastructure.Persistence.Configurations
{
    internal sealed class CountyConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable(TableNames.Country);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
              .HasColumnName("id")
              .ValueGeneratedNever();

            builder.Property(c => c.Name)
              .HasColumnName("name")
              .HasConversion(
                  genderName => genderName.Value,
                  dbValue => CountryName.Create(dbValue))
              .HasMaxLength(CountryName.MAX_LENGTH)
              .UseCollation(PostgresConstants.COLLATION_NAME)
              .IsRequired();
        }
    }
}