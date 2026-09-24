using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Domain.ValueObjects.User;
using Shared.Kernel.Interfaces;

namespace Nexus.UserManagement.Application.Abstractions.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> CheckAvailableEmail(Email email);
        Task<bool> ExistFriendshipCode(FriendshipCode code, CancellationToken cancellationToken);
    }
}