using Crossdyne.Toolkit.Primitives;
using Crossdyne.Toolkit.Results;
using Microsoft.AspNetCore.Mvc;
using Nexus.Bff.Features.Users.Models;
using Nexus.Bff.Infrastructure.Clients;
using Nexus.Bff.Infrastructure.Clients.UserManagement;
using Shared.Contracts.FileService;
using Shared.Contracts.SocialGraph;
using Shared.Contracts.SocialGraph.Responses;
using Shared.Contracts.UserManagement.Requests;
using Shared.Contracts.UserManagement.Responses;
using Shared.Web.Extensions;

namespace Nexus.Bff.Features.Users
{
    public static class FriendEndpoints
    {
        public static void MapFriendEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost("friends/request", async (
                [FromBody] BffSendFriendRequest request, 
                [FromServices] ISocialGraphClient client) =>
            {
                Result<Unit> result = await client.SendFriendRequest(request);
                
                if (result.IsFailure)
                    return result.Errors.MapToMinimalApiResult();

                return Results.Ok();
            });

            builder.MapPost("friends/decline", async (
                [FromBody] DeclineFriendRequest request, 
                [FromServices] ISocialGraphClient client) =>
            {
                Result<Unit> result = await client.DeclineFriendRequest(request);
                
                if (result.IsFailure)
                    return result.Errors.MapToMinimalApiResult();

                return Results.Ok();
                
            });

            builder.MapPost("friends/cancel", async (
                [FromBody] CancelFriendRequest request, 
                [FromServices] ISocialGraphClient client) =>
            {
                Result<Unit> result = await client.CancelFriendRequest(request);
                
                if (result.IsFailure)
                    return result.Errors.MapToMinimalApiResult();

                return Results.Ok();
            });

            builder.MapPost("friends/accept", async (
                [FromBody] AcceptFriendRequest request, 
                [FromServices] ISocialGraphClient client) =>
            {
                Result<Unit> result = await client.AcceptFriendRequest(request);
                
                if (result.IsFailure)
                    return result.Errors.MapToMinimalApiResult();

                return Results.Ok();
            });

            builder.MapGet("friends/incoming", async (
                [FromServices] ISocialGraphClient socialGraphClient,
                [FromServices] IUserManagementService userManagementService,
                [FromServices] IFileService fileService) =>
            {
                Result<List<IncomingFriendResponse>> result = await socialGraphClient.IncomingFriendRequests();
                Result<List<RequestsInfoResponse>> requestsInfo = await userManagementService.RequestsInfo(new RequestsInfoRequest(result.Value.Select(request => Guid.Parse(request.UserId)).ToList()));

                List<RequestsInfoResponse> targetUsers = requestsInfo.Value;
                List<RequestsInfoResponse> usersWithAvatars = [.. targetUsers.Where(u => u.AvatarKey != null)];
                List<FileRequest> fileRequests = [.. usersWithAvatars.Select(u => new FileRequest(u.AvatarKey!.Bucket, u.AvatarKey.FolderPath, u.AvatarKey.Key))];
                
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

                List<BffIncomingFriendResponse> searches = targetUsers.Select(u =>
                {
                    string? avatarUrl = null;

                    if (u.AvatarKey != null && urlLookup.TryGetValue(u.AvatarKey.Key, out var url))
                        avatarUrl = url;

                    return new BffIncomingFriendResponse(u.UserId!, u.UserName!, avatarUrl);
                }).ToList();

                return Results.Ok(searches);
            });

            builder.MapGet("friends/outgoing", async (
                [FromServices] ISocialGraphClient socialGraphClient,
                [FromServices] IUserManagementService userManagementService,
                [FromServices] IFileService fileService) =>
            {
                Result<List<OutgoingFriendResponse>> result = await socialGraphClient.OutgoingFriendRequests();
                Result<List<RequestsInfoResponse>> requestsInfo = await userManagementService.RequestsInfo(new RequestsInfoRequest(result.Value.Select(request => Guid.Parse(request.UserId)).ToList()));

                List<RequestsInfoResponse> targetUsers = requestsInfo.Value;
                List<RequestsInfoResponse> usersWithAvatars = [.. targetUsers.Where(u => u.AvatarKey != null)];
                List<FileRequest> fileRequests = [.. usersWithAvatars.Select(u => new FileRequest(u.AvatarKey!.Bucket, u.AvatarKey.FolderPath, u.AvatarKey.Key))];
                
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

                List<BffOutgoingFriendResponse> searches = targetUsers.Select(u =>
                {
                    string? avatarUrl = null;

                    if (u.AvatarKey != null && urlLookup.TryGetValue(u.AvatarKey.Key, out var url))
                        avatarUrl = url;

                    return new BffOutgoingFriendResponse(u.UserId!, u.UserName!, avatarUrl);
                }).ToList();

                return Results.Ok(searches);
            });

            builder.MapGet("friends", async (
                [FromServices] ISocialGraphClient socialGraphClient,
                [FromServices] IUserManagementService userManagementService,
                [FromServices] IFileService fileService) =>
            {
                Result<List<FriendResponse>> result = await socialGraphClient.Friends();
                Result<List<RequestsInfoResponse>> requestsInfo = await userManagementService.RequestsInfo(new RequestsInfoRequest(result.Value.Select(request => Guid.Parse(request.UserId)).ToList()));

                List<RequestsInfoResponse> targetUsers = requestsInfo.Value;
                List<RequestsInfoResponse> usersWithAvatars = [.. targetUsers.Where(u => u.AvatarKey != null)];
                List<FileRequest> fileRequests = [.. usersWithAvatars.Select(u => new FileRequest(u.AvatarKey!.Bucket, u.AvatarKey.FolderPath, u.AvatarKey.Key))];
                
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

                List<BffFriendResponse> searches = targetUsers.Select(u =>
                {
                    string? avatarUrl = null;

                    if (u.AvatarKey != null && urlLookup.TryGetValue(u.AvatarKey.Key, out var url))
                        avatarUrl = url;

                    return new BffFriendResponse(u.UserId!, u.UserName!, avatarUrl);
                }).ToList();

                return Results.Ok(searches);
            });
        }
    }
}