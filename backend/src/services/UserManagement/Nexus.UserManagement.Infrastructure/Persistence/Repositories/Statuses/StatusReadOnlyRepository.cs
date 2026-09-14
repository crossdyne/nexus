using System.Data;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Infrastructure.Persistence.Constants;
using Shared.Contracts.UserManagement.Responses;
using Shared.Dapper;

namespace Nexus.UserManagement.Infrastructure.Persistence.Repositories.Statuses
{
    internal sealed class StatusReadOnlyRepository(IDbConnection connection) : ReadOnlyRepository<StatusResponse>(connection, TableNames.Status), IStatusReadOnlyRepository
    {
        
    }
}