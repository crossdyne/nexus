using Microsoft.Extensions.DependencyInjection;
using Nexus.Authentication.Application.Abstractions.Repositories;
using Nexus.Authentication.Infrastructure.Persistence.Repositories.AccessDatas;

namespace Nexus.Authentication.Infrastructure.Extensions
{
    public static class RepositoryCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccessDataRepository, AccessDataRepository>();

            return services;
        }
    }
}