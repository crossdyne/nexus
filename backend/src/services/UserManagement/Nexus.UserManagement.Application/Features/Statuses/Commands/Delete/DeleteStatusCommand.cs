using MediatR;
using Crossdyne.Toolkit.Results;
using Nexus.UserManagement.Application.Abstractions.Messaging;

namespace Nexus.UserManagement.Application.Features.Statuses.Commands.Delete
{
    public sealed record DeleteStatusCommand(Guid Id) : IRequest<Result>, ICommand;
}