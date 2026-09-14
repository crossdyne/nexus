using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Infrastructure.Persistence.Contexts;
using Shared.EntityFramework;

namespace Nexus.UserManagement.Infrastructure.Persistence.Repositories.Roles
{
    internal sealed class RoleRepository(UserManagementContext context) : Repository<Role, UserManagementContext>(context), IRoleRepository
    {
        
    }
}