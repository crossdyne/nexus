using MediatR;
using Crossdyne.Toolkit.Results;
using Shared.Contracts.UserManagement.Responses;
using Nexus.UserManagement.Application.Abstractions.Messaging;

namespace Nexus.UserManagement.Application.Features.Users.Queries.GetPublicEncryptionInnfo
{
    public sealed record GetPublicEncryptionInfoQuery(string Login) : IRequest<Result<PublicEncryptionInfoResponse>>, IQuery;
}