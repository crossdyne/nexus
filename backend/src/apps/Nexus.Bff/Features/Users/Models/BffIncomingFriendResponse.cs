namespace Nexus.Bff.Features.Users.Models
{
    public sealed record BffIncomingFriendResponse(string UserId, string UserName, string? AvatarUrl);
}