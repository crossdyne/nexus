namespace Nexus.UserManagement.Infrastructure.Persistence.Repositories.Users.Models
{
    public sealed record RequestsInfo(string UserId, string UserName, string? AvatarKey);
}