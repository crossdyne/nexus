using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Infrastructure.Persistence.Contexts;
using Shared.EntityFramework;

namespace Nexus.UserManagement.Infrastructure.Persistence.Repositories.Countries
{
    internal sealed class CountryRepository(UserManagementContext context) : Repository<Country, UserManagementContext>(context), ICountryRepository
    {
        
    }
}