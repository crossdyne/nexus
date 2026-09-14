using Crossdyne.Toolkit.Results;
using MediatR;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Service.Application.Features.Users.Queries.SearchUsers
{
    public sealed record SearchUsersQuery(string? Input) : IRequest<Result<List<SearchUserResponse>>>;
}