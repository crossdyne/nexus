using MediatR;
using Crossdyne.Toolkit.Results;
using Crossdyne.Toolkit.Primitives;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Application.Abstractions.UnitOfWork;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Domain.ValueObjects.Role;

namespace Nexus.UserManagement.Application.Features.Roles.Commands.Update
{
    public sealed class UpdateRoleCommandHandler(
        IUnitOfWork unitOfWork, 
        IRoleRepository roleRepository) : IRequestHandler<UpdateRoleCommand, Result>
    {
        public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            Maybe<Role> maybeRole = await roleRepository.GetByAsync(r => r.Id == request.Id, cancellationToken);

            if (maybeRole.IsNone)
                return Result.Failure(new Error(ErrorCode.Delete, "Такой записи не существует."));

            Role role = maybeRole.Value;

            role.UpdateName(RoleName.Create(request.Name));

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}