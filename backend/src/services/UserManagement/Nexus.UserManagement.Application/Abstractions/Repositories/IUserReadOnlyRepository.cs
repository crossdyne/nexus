using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Abstractions.Repositories
{
    public interface IUserReadOnlyRepository
    {
        Task<UserAuthDataResponse> GetUserByIdAuth(Guid userId);
        Task<UserAuthDataResponse> GetUserByLoginAuth(string login);
        Task<ByInviteCodeResponse> GetByInviteCode(string inviteCode);
        Task<ProfileInfoResponse> GetProfileInfo(Guid userId);
        Task<PublicEncryptionInfoResponse> GetPublicEncryptionInfoResponse(string login);
        Task<DekResponse> GetDek(Guid userId);
        Task<GetChangePasswordDataResponse> GetChangePasswordData(Guid userId);
        Task<RecoveryViaKeysPayloadResponse> GetRecoveryKeys(string login);
        Task<bool> ExistUserByLoginAsync(string login);
        Task<List<SearchUserResponse>> Search(string input, string notIncludeLogin);
        Task<List<RequestsInfoResponse>> RequestsInfo(List<Guid> userIds);
    }
}