using Shared.Contracts.UserManagement.Responses;

namespace Nexus.Authentication.Application.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateAccessToken(UserAuthDataResponse user);
        string GenerateRefreshToken();
    }
}
