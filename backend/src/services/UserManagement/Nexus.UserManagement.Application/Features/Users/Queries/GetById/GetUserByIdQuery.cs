using MediatR;
using Nexus.UserManagement.Application.Abstractions.Messaging;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Users.Queries.GetById
{
    public sealed record GetUserByIdQuery(Guid UserId) : IRequest<UserAuthDataResponse>, IQuery;
}