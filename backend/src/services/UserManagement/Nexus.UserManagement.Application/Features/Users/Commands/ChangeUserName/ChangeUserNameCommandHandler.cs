using Crossdyne.Toolkit.Primitives;
using Crossdyne.Toolkit.Results;
using MediatR;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Application.Abstractions.UnitOfWork;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Domain.ValueObjects.User;
using Unit = Crossdyne.Toolkit.Primitives.Unit;

namespace Nexus.UserManagement.Application.Features.Users.Commands.ChangeUserName
{
    public sealed class ChangeUserNameCommandHandler(
        IUserRepository repository,
        IUnitOfWork unitOfWork) : IRequestHandler<ChangeUserNameCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(ChangeUserNameCommand request, CancellationToken cancellationToken)
        {
            Maybe<User> maybe = await repository.GetByAsync(u => u.Id == request.UserId);

            if (maybe.IsNone)
                return new Error(ErrorCode.NotFound, "Пользователь не найден");

            User user = maybe.Value;

            user.ChangeUserName(UserName.Create(request.UserName), changedByUserId: null);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}