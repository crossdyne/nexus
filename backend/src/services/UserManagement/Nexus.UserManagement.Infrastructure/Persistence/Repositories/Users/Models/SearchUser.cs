namespace Nexus.UserManagement.Infrastructure.Persistence.Repositories.Users.Models
{
    public sealed record SearchUser(string UserId, string FriendshipCode, string UserName, string AvatarKey);
}