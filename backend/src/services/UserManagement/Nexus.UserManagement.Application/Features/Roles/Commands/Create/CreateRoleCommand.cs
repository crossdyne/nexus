using MediatR;
using Crossdyne.Toolkit.Results;
using Nexus.UserManagement.Application.Abstractions.Messaging;

namespace Nexus.UserManagement.Application.Features.Roles.Commands.Create
{
    public sealed record CreateRoleCommand(string Name) : IRequest<Result<Guid>>, ICommand;
}