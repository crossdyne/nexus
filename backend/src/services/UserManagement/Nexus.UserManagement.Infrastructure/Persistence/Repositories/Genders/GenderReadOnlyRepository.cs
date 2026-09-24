using System.Data;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Infrastructure.Persistence.Constants;
using Shared.Contracts.UserManagement.Responses;
using Shared.Dapper;

namespace Nexus.UserManagement.Infrastructure.Persistence.Repositories.Genders
{
    internal sealed class GenderReadOnlyRepository(IDbConnection connection) : ReadOnlyRepository<GenderResponse>(connection, TableNames.Gender), IGenderReadOnlyRepository
    {
        
    }
}