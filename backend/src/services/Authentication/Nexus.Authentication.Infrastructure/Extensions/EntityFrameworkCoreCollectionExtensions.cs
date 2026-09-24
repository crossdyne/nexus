using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Authentication.Application.Abstractions.UnitOfWork;
using Nexus.Authentication.Infrastructure.Persistence;
using Nexus.Authentication.Infrastructure.Persistence.Contexts;

namespace Nexus.Authentication.Infrastructure.Extensions
{
    public static class EntityFrameworkCoreCollectionExtensions
    {
        public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AuthenticationContext>(option => option.UseNpgsql(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}