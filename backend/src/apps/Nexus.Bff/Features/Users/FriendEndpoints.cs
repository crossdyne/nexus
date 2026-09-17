using Crossdyne.Toolkit.Primitives;
using Crossdyne.Toolkit.Results;
using Microsoft.AspNetCore.Mvc;
using Nexus.Bff.Features.Users.Models;
using Nexus.Bff.Infrastructure.Clients;
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
        }
    }
}