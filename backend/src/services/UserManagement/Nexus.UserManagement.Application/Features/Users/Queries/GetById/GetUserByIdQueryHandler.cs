using MediatR;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Users.Queries.GetById
{
    public sealed class GetUserByIdQueryHandler(IUserReadOnlyRepository userRepository) : IRequestHandler<GetUserByIdQuery, UserAuthDataResponse>
    {
        public async Task<UserAuthDataResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            => await userRepository.GetUserByIdAuth(request.UserId);
    }
}