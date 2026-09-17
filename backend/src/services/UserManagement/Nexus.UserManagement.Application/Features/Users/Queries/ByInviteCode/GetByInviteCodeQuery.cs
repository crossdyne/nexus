using MediatR;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Users.Queries.ByInviteCode
{
    public sealed record GetByInviteCodeQuery(string InviteCode) : IRequest<ByInviteCodeResponse>;
}