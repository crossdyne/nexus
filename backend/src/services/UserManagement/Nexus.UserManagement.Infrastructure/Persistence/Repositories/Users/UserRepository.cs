using Microsoft.EntityFrameworkCore;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Domain.ValueObjects.User;
using Nexus.UserManagement.Infrastructure.Persistence.Contexts;
using Shared.EntityFramework;

namespace Nexus.UserManagement.Infrastructure.Persistence.Repositories.Users
{
    internal sealed class UserRepository(UserManagementContext context) : Repository<User, UserManagementContext>(context), IUserRepository
    {
        public async Task<bool> CheckAvailableEmail(Email email) => await _entity.AnyAsync(u => u.Email == email);

        public async Task<bool> ExistFriendshipCode(FriendshipCode code, CancellationToken cancellationToken) => await _entity.AnyAsync(u => u.FriendshipCode == code, cancellationToken);
    }
}