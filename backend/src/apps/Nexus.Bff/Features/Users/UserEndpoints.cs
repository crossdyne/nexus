using Crossdyne.Toolkit.Results;
using Microsoft.AspNetCore.Mvc;
using Nexus.Bff.Features.Users.Models;
using Nexus.Bff.Infrastructure.Clients;
using Nexus.Bff.Infrastructure.Clients.UserManagement;
using Shared.Contracts.FileService;
using Shared.Contracts.SocialGraph;
using Shared.Contracts.UserManagement.Requests;
using Shared.Contracts.UserManagement.Responses;
using Shared.Web.Extensions;

namespace Nexus.Bff.Features.Users
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", async (
                [FromBody] RegisterUserRequest request, 
                [FromServices] IUserManagementService userManagementService, 
                CancellationToken ct) =>
            {
                var result = await userManagementService.Register(new RegisterUserRequest(
                    request.Login.ToLowerInvariant(),
                    request.UserName, 
                    request.Email, 
                    request.IdGender?.ToString(),
                    request.IdCountry?.ToString(),
                    request.EncryptedVerifier, 
                    request.SrpSalt, 
                    request.SrpVersion, 
                    request.SrpCryptoVersion,
                    request.EncryptedVerifierWrapKey,
                    request.KeyWrapVersion, 
                    request.AsymmetricKeyId,
                    request.EncryptedDek, 
                    request.DekSalt, 
                    request.CryptoVersion,
                    [.. request.RecoveryKeys.Select(rk => new RecoveryKeyData(rk.EncryptedValue, rk.CryptoVersion))]));

                if (result.IsFailure)
                    return result.Errors.MapToMinimalApiResult();

                return Results.Ok();
            });

            app.MapGet("exist/user/login", async (
                [FromQuery] string login, 
                [FromServices] IUserManagementService userManagementService, 
                CancellationToken ct) =>
            {
                var result = await userManagementService.ExistUserByLogin(login.ToLowerInvariant());

                if (result.IsFailure)
                    return result.Errors.MapToMinimalApiResult();

                return Results.Ok();
            });

            app.MapGet("public/key", async ([FromServices] IAuthClient authClient) =>
            {
                var result = await authClient.GetPublicKey();

                return Results.Ok(new
                {
                    publicKey = result.Value
                });
            });

            app.MapGet("users/search", async (
                [FromQuery] string? input,
                [FromServices] IUserManagementService userManagementService,
                [FromServices] ISocialGraphClient socialGraphClient,
                [FromServices] IFileService fileService) =>
            {
                if(string.IsNullOrWhiteSpace(input))
                    return Results.Ok(new List<BffSearchUserResponse>());

                Result<List<SearchUserResponse>> searchUsersResult = await userManagementService.SearchUsers(input);
                Result<List<IncomingFriendResponse>> incomingFriendResult = await socialGraphClient.IncomingFriendRequests();
                Result<List<OutgoingFriendResponse>> outgoingFriendResult = await socialGraphClient.OutgoingFriendRequests();

                if (searchUsersResult.IsFailure)
                    return searchUsersResult.Errors.MapToMinimalApiResult();

                List<SearchUserResponse> targetUsers = searchUsersResult.Value;
                List<SearchUserResponse> usersWithAvatars = [.. targetUsers.Where(u => u.AvatarKey != null)];
                List<FileRequest> fileRequests = [.. usersWithAvatars.Select(u => new FileRequest(u.AvatarKey!.Bucket, u.AvatarKey.FolderPath, u.AvatarKey.Key))];
                List<IncomingFriendResponse> incomingFriend = incomingFriendResult.Value;
                List<OutgoingFriendResponse> outgoingFriend = outgoingFriendResult.Value;

                var urlLookup = new Dictionary<string, string>();
                
                if (fileRequests.Count > 0)
                {
                    Result<BatchUrlResponse> urlsResult = await fileService.GetUrls(new BatchUrlRequest(fileRequests, Expires: null));

                    if (urlsResult.IsSuccess && urlsResult.Value.Urls.Count > 0)
                    {
                        List<FileUrl> urls = urlsResult.Value.Urls;
                        foreach (var url in urls)
                        {
                            urlLookup[url.Key] = url.Url;
                        }
                    }
                }

                List<BffSearchUserResponse> searches = targetUsers.Select(u =>
                {
                    string? avatarUrl = u.AvatarKey != null && urlLookup.TryGetValue(u.AvatarKey.Key, out var url) ? url : null;
                    bool meSendRequest = incomingFriend.Select(inc => inc.UserId).Contains(u.UserId);
                    bool iSendRequest = outgoingFriend.Select(outg => outg.UserId).Contains(u.UserId);

                    return new BffSearchUserResponse(u.UserId ,u.InviteCode!, u.UserName!, iSendRequest, meSendRequest, avatarUrl);
                }).ToList();

                return Results.Ok(searches);
            });

        }
    }
}