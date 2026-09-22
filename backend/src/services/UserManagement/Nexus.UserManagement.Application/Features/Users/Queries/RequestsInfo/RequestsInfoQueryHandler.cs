using MediatR;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Users.Queries.RequestsInfo
{
    public sealed class RequestsInfoQueryHandler(IUserReadOnlyRepository repository) : IRequestHandler<RequestsInfoQuery, List<RequestsInfoResponse>>
    {
        public async Task<List<RequestsInfoResponse>> Handle(RequestsInfoQuery request, CancellationToken cancellationToken)
            => await repository.RequestsInfo(request.UserIds);
    }
}