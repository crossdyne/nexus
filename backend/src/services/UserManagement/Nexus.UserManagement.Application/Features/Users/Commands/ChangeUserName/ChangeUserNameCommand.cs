using Crossdyne.Toolkit.Results;
using MediatR;
using Nexus.UserManagement.Application.Abstractions.Messaging;
using Nexus.UserManagement.Application.Abstractions.Validators;
using Unit = Crossdyne.Toolkit.Primitives.Unit;

namespace Nexus.UserManagement.Application.Features.Users.Commands.ChangeUserName
{
    public sealed record ChangeUserNameCommand(Guid UserId, string UserName) : IRequest<Result<Unit>>, ICommand, IHasUserName;
}