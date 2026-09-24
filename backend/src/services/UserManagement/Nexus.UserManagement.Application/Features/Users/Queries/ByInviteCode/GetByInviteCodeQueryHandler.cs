using MediatR;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Users.Queries.ByInviteCode
{
    public sealed class GetByInviteCodeQueryHandler(IUserReadOnlyRepository repository) : IRequestHandler<GetByInviteCodeQuery, ByInviteCodeResponse>
    {
        public async Task<ByInviteCodeResponse> Handle(GetByInviteCodeQuery request, CancellationToken cancellationToken)
            => await repository.GetByInviteCode(request.InviteCode);
    }

}