namespace Nexus.Bff.Features.Users.Models
{
    public sealed record BffFriendResponse(string UserId, string UserName, string? AvatarUrl);
}