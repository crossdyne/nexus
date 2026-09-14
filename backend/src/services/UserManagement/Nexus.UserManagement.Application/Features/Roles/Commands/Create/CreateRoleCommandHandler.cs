using MediatR;
using Crossdyne.Toolkit.Results;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Application.Abstractions.UnitOfWork;
using Nexus.UserManagement.Domain.Models;

namespace Nexus.UserManagement.Application.Features.Roles.Commands.Create
{
    public sealed class CreateRoleCommandHandler(
        IUnitOfWork unitOfWork, 
        IRoleRepository roleRepository) : IRequestHandler<CreateRoleCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = Role.Create(request.Name);

            await roleRepository.AddAsync(role, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return role.Id.Value;
        }
    }
}