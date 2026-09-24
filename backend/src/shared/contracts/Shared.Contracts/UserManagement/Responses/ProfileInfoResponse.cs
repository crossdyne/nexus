namespace Shared.Contracts.UserManagement.Responses
{
    public sealed record ProfileInfoResponse(
        string Login, 
        string UserName,
        string Email, 
        DateTime DateRegistration,
        string FriendshipCode,
        S3KeyResponse? AvatarS3Key);
}