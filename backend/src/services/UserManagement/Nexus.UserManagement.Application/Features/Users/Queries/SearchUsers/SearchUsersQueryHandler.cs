using Crossdyne.Toolkit.Results;
using MediatR;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Users.Queries.SearchUsers
{
    public sealed class SearchUsersQueryHandler(
        IUserReadOnlyRepository repository) : IRequestHandler<SearchUsersQuery, Result<List<SearchUserResponse>>>
    {
        public async Task<Result<List<SearchUserResponse>>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Input))
                return new List<SearchUserResponse>();

            return await repository.Search(request.Input, request.NotIncludeUserLogin);
        }
    }
}