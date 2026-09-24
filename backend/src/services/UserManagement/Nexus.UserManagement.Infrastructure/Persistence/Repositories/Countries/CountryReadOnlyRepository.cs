using System.Data;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Infrastructure.Persistence.Constants;
using Shared.Contracts.UserManagement.Responses;
using Shared.Dapper;

namespace Nexus.UserManagement.Infrastructure.Persistence.Repositories.Countries
{
    internal sealed class CountryReadOnlyRepository(IDbConnection connection) : ReadOnlyRepository<CountryResponse>(connection, TableNames.Country) ,ICountryReadOnlyRepository
    {
        
    }
}