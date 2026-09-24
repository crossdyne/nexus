using MediatR;
using Crossdyne.Toolkit.Results;
using Nexus.UserManagement.Application.Abstractions.Messaging;

namespace Nexus.UserManagement.Application.Features.Statuses.Commands.Create
{
    public sealed record CreateStatusCommand(string Name) : IRequest<Result<Guid>>, ICommand;
}