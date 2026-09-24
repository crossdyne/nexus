using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Infrastructure.Persistence.Contexts;
using Shared.EntityFramework;

namespace Nexus.UserManagement.Infrastructure.Persistence.Repositories.Statuses
{
    internal sealed class StatusRepository(UserManagementContext context) : Repository<Status, UserManagementContext>(context), IStatusRepository
    {
        
    }
}