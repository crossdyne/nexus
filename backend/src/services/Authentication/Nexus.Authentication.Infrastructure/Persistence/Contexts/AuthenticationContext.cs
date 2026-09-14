using Microsoft.EntityFrameworkCore;
using Nexus.Authentication.Domain.Models;
using System.Reflection;

namespace Nexus.Authentication.Infrastructure.Persistence.Contexts
{
    public sealed class AuthenticationContext(DbContextOptions<AuthenticationContext> potions) : DbContext(potions)
    {
        public DbSet<AccessData> AccessData { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}