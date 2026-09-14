using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Infrastructure.Persistence.Contexts;
using Shared.EntityFramework;

namespace Nexus.UserManagement.Infrastructure.Persistence.Repositories.Genders
{
    internal sealed class GenderRepository(UserManagementContext context) : Repository<Gender, UserManagementContext>(context), IGenderRepository
    {
        
    }
}