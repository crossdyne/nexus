using Shared.Contracts.UserManagement.Responses;

namespace Nexus.Authentication.Application.Abstractions.Clients
{
    public interface IUserManagementServiceClient
    {
        Task<UserAuthDataResponse?> GetUserByIdAsync(Guid userId);
        Task<UserAuthDataResponse?> GetUserByLoginAsync(string login);
    }
}