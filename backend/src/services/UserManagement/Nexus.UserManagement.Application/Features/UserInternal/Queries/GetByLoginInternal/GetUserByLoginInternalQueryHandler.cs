using MediatR;
using Crossdyne.Toolkit.Results;
using Shared.Contracts.UserManagement.Responses;
using Nexus.UserManagement.Application.Abstractions.Repositories;

namespace Nexus.UserManagement.Application.Features.UserInternal.Queries.GetByLoginInternal
{
    public sealed class GetUserByLoginInternalQueryHandler(IUserReadOnlyRepository userRepository) : IRequestHandler<GetUserByLoginInternalQuery, Result<UserAuthDataResponse>>
    {
        public async Task<Result<UserAuthDataResponse>> Handle(GetUserByLoginInternalQuery request, CancellationToken cancellationToken)
            => await userRepository.GetUserByLoginAuth(request.Login);
    }
}