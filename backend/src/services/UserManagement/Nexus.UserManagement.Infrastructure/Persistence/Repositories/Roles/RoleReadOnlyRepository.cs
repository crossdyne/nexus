using System.Data;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Infrastructure.Persistence.Constants;
using Shared.Contracts.UserManagement.Responses;
using Shared.Dapper;

namespace Nexus.UserManagement.Infrastructure.Persistence.Repositories.Roles
{
    internal sealed class RoleReadOnlyRepository(IDbConnection connection) : ReadOnlyRepository<RoleResponse>(connection, TableNames.Role), IRoleReadOnlyRepository
    {
        
    }
}