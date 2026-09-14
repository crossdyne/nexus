namespace Shared.Contracts.UserManagement.Responses
{
    public sealed record SearchUserResponse(string InviteCode, string UserName, S3KeyResponse? AvatarKey);
}