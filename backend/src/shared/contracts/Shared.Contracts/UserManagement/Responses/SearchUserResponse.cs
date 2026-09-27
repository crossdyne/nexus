namespace Shared.Contracts.UserManagement.Responses
{
    public sealed record SearchUserResponse(string UserId, string UserName, S3KeyResponse? AvatarKey);
}