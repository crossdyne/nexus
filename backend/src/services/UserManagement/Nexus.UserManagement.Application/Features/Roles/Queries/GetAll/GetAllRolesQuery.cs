using MediatR;
using Nexus.UserManagement.Application.Abstractions.Messaging;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Roles.Queries.GetAll
{
    public sealed record GetAllRolesQuery() : IRequest<List<RoleResponse>>, IQuery;
}