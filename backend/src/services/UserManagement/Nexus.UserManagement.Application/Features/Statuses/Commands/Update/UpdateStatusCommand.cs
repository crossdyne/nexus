using MediatR;
using Crossdyne.Toolkit.Results;
using Nexus.UserManagement.Application.Abstractions.Messaging;

namespace Nexus.UserManagement.Application.Features.Statuses.Commands.Update
{
    public sealed record UpdateStatusCommand(Guid Id, string Name) : IRequest<Result>, ICommand;
}