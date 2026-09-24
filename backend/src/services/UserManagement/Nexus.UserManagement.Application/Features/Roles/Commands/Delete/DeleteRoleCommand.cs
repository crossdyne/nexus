using MediatR;
using Crossdyne.Toolkit.Results;
using Nexus.UserManagement.Application.Abstractions.Messaging;

namespace Nexus.UserManagement.Application.Features.Roles.Commands.Delete
{
    public sealed record DeleteRoleCommand(Guid Id) : IRequest<Result>, ICommand;
}