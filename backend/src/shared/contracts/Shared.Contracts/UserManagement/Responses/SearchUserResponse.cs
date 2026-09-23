namespace Shared.Contracts.UserManagement.Responses
{
    public sealed record SearchUserResponse(string UserId, string InviteCode, string UserName, S3KeyResponse? AvatarKey);
}