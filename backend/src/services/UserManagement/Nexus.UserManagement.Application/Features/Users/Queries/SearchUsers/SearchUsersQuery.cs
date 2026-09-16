using Crossdyne.Toolkit.Results;
using MediatR;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Users.Queries.SearchUsers
{
    public sealed record SearchUsersQuery(string? Input, string NotIncludeUserLogin) : IRequest<Result<List<SearchUserResponse>>>;
}