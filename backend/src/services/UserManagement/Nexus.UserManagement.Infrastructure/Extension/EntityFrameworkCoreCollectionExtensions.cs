using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nexus.UserManagement.Application.Abstractions.Transactions;
using Nexus.UserManagement.Application.Abstractions.UnitOfWork;
using Nexus.UserManagement.Infrastructure.Persistence;
using Nexus.UserManagement.Infrastructure.Persistence.Contexts;

namespace Nexus.UserManagement.Infrastructure.Extension
{
    public static class EntityFrameworkCoreCollectionExtensions
    {
        public static IServiceCollection RegisterWriteDatabase(this IServiceCollection services, string dateBaseConnectionString)
        {
            services.AddDbContext<UserManagementContext>(option => option.UseNpgsql(dateBaseConnectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITransactionManager, EfTransactionManager>();

            return services;
        } 
    }
}