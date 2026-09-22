using Crossdyne.Toolkit.Primitives;
using Crossdyne.Toolkit.Results;
using Nexus.Bff.Features.Users.Models;
using Shared.Contracts.SocialGraph;

namespace Nexus.Bff.Infrastructure.Clients
{
    public interface ISocialGraphClient
    {
        Task<Result<Unit>> SendFriendRequest(BffSendFriendRequest request);
        Task<Result<List<IncomingFriendResponse>>> IncomingFriendRequests();
        Task<Result<List<OutgoingFriendResponse>>> OutgoingFriendRequests();
    }
}