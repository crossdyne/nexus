namespace Nexus.Bff.Features.Users.Models
{
    public sealed record BffOutgoingFriendResponse(string UserId, string UserName, string? AvatarUrl);
}