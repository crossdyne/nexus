using MediatR;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Roles.Queries.GetAll
{
    public sealed class GetAllRolesQueryHandler(IRoleReadOnlyRepository roleRepository) : IRequestHandler<GetAllRolesQuery, List<RoleResponse>>
    {
        public async Task<List<RoleResponse>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
            => [.. await roleRepository.GetAllAsync(cancellationToken)];
            
    }
}