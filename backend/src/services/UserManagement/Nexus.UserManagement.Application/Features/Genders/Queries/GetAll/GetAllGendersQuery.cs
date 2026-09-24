using MediatR;
using Nexus.UserManagement.Application.Abstractions.Messaging;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Genders.Queries.GetAll
{
    public sealed record GetAllGendersQuery() : IRequest<List<GenderResponse>>, IQuery;
}