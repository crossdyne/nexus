using Crossdyne.Toolkit.Results;
using MediatR;
using Nexus.UserManagement.Application.Abstractions.Messaging;
using Unit = Crossdyne.Toolkit.Primitives.Unit;

namespace Nexus.UserManagement.Application.Features.Users.Commands.ChangeAvatar
{
    public sealed record ChangeAvatarCommand(Guid UserId, Stream File, string FileName) : IRequest<Result<Unit>>, ICommand;
}