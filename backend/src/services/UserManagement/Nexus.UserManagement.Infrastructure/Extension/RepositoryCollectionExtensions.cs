using Microsoft.Extensions.DependencyInjection;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Infrastructure.Persistence.Repositories.Countries;
using Nexus.UserManagement.Infrastructure.Persistence.Repositories.Genders;
using Nexus.UserManagement.Infrastructure.Persistence.Repositories.Roles;
using Nexus.UserManagement.Infrastructure.Persistence.Repositories.Statuses;
using Nexus.UserManagement.Infrastructure.Persistence.Repositories.Users;

namespace Nexus.UserManagement.Infrastructure.Extension
{
    public static class RepositoryCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICountryReadOnlyRepository, CountryReadOnlyRepository>();

            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IGenderReadOnlyRepository, GenderReadOnlyRepository>();

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleReadOnlyRepository, RoleReadOnlyRepository>();

            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IStatusReadOnlyRepository, StatusReadOnlyRepository>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();

            return services;
        }
    }
}