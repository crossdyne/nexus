using Crossdyne.Toolkit.Results;
using MediatR;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Users.Queries.GetChangePasswordData
{
    public sealed class GetChangePasswordDataQueryHandler(IUserReadOnlyRepository userRepository) : IRequestHandler<GetChangePasswordDataQuery, Result<GetChangePasswordDataResponse>>
    {
        public async Task<Result<GetChangePasswordDataResponse>> Handle(GetChangePasswordDataQuery request, CancellationToken cancellationToken)
            => await userRepository.GetChangePasswordData(request.UserId);
    }
}