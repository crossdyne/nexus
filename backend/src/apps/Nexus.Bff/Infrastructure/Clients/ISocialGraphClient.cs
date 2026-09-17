using Crossdyne.Toolkit.Primitives;
using Crossdyne.Toolkit.Results;
using Nexus.Bff.Features.Users.Models;

namespace Nexus.Bff.Infrastructure.Clients
{
    public interface ISocialGraphClient
    {
        Task<Result<Unit>> SendFriendRequest(BffSendFriendRequest request);
    }
}