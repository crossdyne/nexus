using MediatR;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Users.Queries.RequestsInfo
{
    public sealed record RequestsInfoQuery(List<Guid> UserIds) : IRequest<List<RequestsInfoResponse>>;
}