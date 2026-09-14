using MediatR;
using Nexus.UserManagement.Application.Abstractions.Messaging;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Statuses.Queries.GetAll
{
    public sealed record GetAllStatusesQuery() : IRequest<List<StatusResponse>>, IQuery;
}