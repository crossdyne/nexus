namespace Nexus.Bff.Features.Users.Models
{
    public sealed record BffSearchUserResponse(string UserId, string InviteCode, string UserName, bool IsISend, bool IsIWasSend, bool IsFriend, string? AvatarUrl);
}