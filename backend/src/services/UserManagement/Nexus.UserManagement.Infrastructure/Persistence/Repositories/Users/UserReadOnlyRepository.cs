using System.Data;
using Dapper;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Domain.ValueObjects.Common;
using Nexus.UserManagement.Infrastructure.Helpers;
using Nexus.UserManagement.Infrastructure.Persistence.Repositories.Users.Models;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Infrastructure.Persistence.Repositories.Users
{
    internal sealed class UserReadOnlyRepository(IDbConnection connection) : IUserReadOnlyRepository
    {        
        public async Task<UserAuthDataResponse> GetUserByIdAuth(Guid userId)
        {
            var sql = SqlLoader.Load("Users", "GetUserByIdAuth");
            var user = await connection.QueryFirstOrDefaultAsync<UserAuthDataResponse>(sql, new { userId });

            return user!;
        }

        public async Task<UserAuthDataResponse> GetUserByLoginAuth(string login)
        {
            var sql = SqlLoader.Load("Users", "GetUserByLoginAuth");
            var user = await connection.QueryFirstOrDefaultAsync<UserAuthDataResponse>(sql, new { login });

            return user!;
        }

        public async Task<ByInviteCodeResponse> GetByInviteCode(string inviteCode)
        {
            var sql = SqlLoader.Load("Users", "GetUserByInviteCode");
            var user = await connection.QueryFirstOrDefaultAsync<ByInviteCodeResponse>(sql, new { inviteCode });

            return user!;
        }

        public async Task<ProfileInfoResponse> GetProfileInfo(Guid userId)
        {
            var sql = SqlLoader.Load("Users", "GetProfileInfo");
            var profileInfo = await connection.QueryFirstOrDefaultAsync<ProfileInfoResponse>(sql, new { userId });

            return profileInfo!;
        }

        public async Task<PublicEncryptionInfoResponse> GetPublicEncryptionInfoResponse(string login)
        {
            var sql = SqlLoader.Load("Users", "GetPublicEncryptionInfo");
            var info = await connection.QueryFirstOrDefaultAsync<PublicEncryptionInfoResponse>(sql, new { login });

            return info!;
        }

        public async Task<DekResponse> GetDek(Guid userId)
        {
            var sql = SqlLoader.Load("Users", "GetDek");
            var dek = await connection.QueryFirstOrDefaultAsync<DekResponse>(sql, new { userId });

            return dek!;
        }

        public async Task<GetChangePasswordDataResponse> GetChangePasswordData(Guid userId)
        {
            var sql = SqlLoader.Load("Users", "GetChangePasswordData");
            var init = await connection.QueryFirstOrDefaultAsync<GetChangePasswordDataResponse>(sql, new { userId });

            return init!;
        }

        public async Task<RecoveryViaKeysPayloadResponse> GetRecoveryKeys(string login)
        {
            var sql = SqlLoader.Load("Users", "GetRecoveryKeys");
            var init = await connection.QueryFirstOrDefaultAsync<RecoveryViaKeysPayloadResponse>(sql, new { login });

            return init!;
        }

        public async Task<bool> ExistUserByLoginAsync(string login)
        {
            var sql = SqlLoader.Load("Users", "ExistUserByLogin");
            var isExist = await connection.ExecuteScalarAsync<bool>(sql, new { login });

            return isExist;
        }

        public async Task<List<SearchUserResponse>> Search(string input, string notIncludeLogin)
        {
            string userName = input
                .Replace("\\", "\\\\")
                .Replace("%", "\\%")
                .Replace("_", "\\_");
            
            var sql = SqlLoader.Load("Users", "SearchUsers");
            IEnumerable<SearchUser> result = await connection.QueryAsync<SearchUser>(sql, new { userName, notIncludeLogin });

            List<SearchUserResponse> users = result.Select(u =>
            {
                S3KeyResponse? avatarKey = null;

                if (u.AvatarKey != null)
                {
                    var key = S3Key.Restore(u.AvatarKey);
                    avatarKey = new S3KeyResponse(key.FileName, key.Bucket, key.FolderPath);
                }
                    
                return new SearchUserResponse(u.FriendshipCode, u.UserName, avatarKey);
            }).ToList();

            return users;
        }
    }
}