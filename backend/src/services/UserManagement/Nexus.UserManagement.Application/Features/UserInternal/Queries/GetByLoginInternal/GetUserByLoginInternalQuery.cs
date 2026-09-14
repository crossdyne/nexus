using MediatR;
using Crossdyne.Toolkit.Results;
using Shared.Contracts.UserManagement.Responses;
using Nexus.UserManagement.Application.Abstractions.Messaging;

namespace Nexus.UserManagement.Application.Features.UserInternal.Queries.GetByLoginInternal
{
    public sealed record GetUserByLoginInternalQuery(string Login) : IRequest<Result<UserAuthDataResponse>>, IQuery;
}