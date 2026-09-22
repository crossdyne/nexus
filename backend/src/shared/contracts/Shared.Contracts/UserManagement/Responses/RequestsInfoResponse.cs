namespace Shared.Contracts.UserManagement.Responses
{
    public sealed record RequestsInfoResponse(string UserId, string UserName, S3KeyResponse? AvatarKey);
}