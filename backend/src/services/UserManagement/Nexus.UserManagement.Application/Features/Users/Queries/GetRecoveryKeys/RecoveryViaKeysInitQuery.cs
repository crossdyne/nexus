using Crossdyne.Toolkit.Results;
using MediatR;
using Nexus.UserManagement.Application.Abstractions.Messaging;
using Shared.Abstractions.Validations;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Users.Queries.GetRecoveryKeys
{
    public sealed record GetRecoveryKeysQuery(string Login) : IRequest<Result<RecoveryViaKeysPayloadResponse>>, IHasLogin, IQuery;
}