using Crossdyne.Toolkit.Results;
using Microsoft.AspNetCore.Mvc;
using Nexus.Bff.Features.Users.Models;
using Nexus.Bff.Infrastructure.Clients;
using Nexus.Bff.Infrastructure.Clients.UserManagement;
using Shared.Contracts.FileService;
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
                [FromServices] IFileService fileService) =>
            {
                Result<List<SearchUserResponse>> searchUsersResult = await userManagementService.SearchUsers(input);

                if (searchUsersResult.IsFailure)
                    return searchUsersResult.Errors.MapToMinimalApiResult();

                var fileRequests = new List<FileRequest>();

                foreach (var search in searchUsersResult.Value)
                    fileRequests.Add(new FileRequest(search?.AvatarKey?.Bucket!, search?.AvatarKey?.FolderPath!, search?.AvatarKey?.Key!));

                var request = new BatchUrlRequest(fileRequests, Expires: null);

                Result<BatchUrlResponse> urlsResult = await fileService.GetUrls(request);

                List<BffSearchUserResponse> searches = [];

                foreach (var user in searchUsersResult.Value)
                {
                    FileUrl? avatarUrl = urlsResult.Value.Urls.FirstOrDefault(url => url.Key == user.AvatarKey?.Key); 
                    searches.Add(new BffSearchUserResponse(user.InviteCode, user.UserName, avatarUrl?.Url));
                }

                return Results.Ok(searches);
            });
        }
    }
}