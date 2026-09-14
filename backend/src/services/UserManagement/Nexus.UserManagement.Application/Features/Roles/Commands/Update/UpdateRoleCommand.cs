using MediatR;
using Crossdyne.Toolkit.Results;
using Nexus.UserManagement.Application.Abstractions.Messaging;

namespace Nexus.UserManagement.Application.Features.Roles.Commands.Update
{
    public sealed record UpdateRoleCommand(Guid Id, string Name) : IRequest<Result>, ICommand;
}