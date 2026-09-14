namespace Nexus.Bff.Features.Users.Models
{
    public sealed record BffSearchUserResponse(string InviteCode, string UserName, string? AvatarUrl);
}