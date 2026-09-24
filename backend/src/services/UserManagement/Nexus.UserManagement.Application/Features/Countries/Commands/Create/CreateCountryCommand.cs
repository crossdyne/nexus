using MediatR;
using Crossdyne.Toolkit.Results;
using Nexus.UserManagement.Application.Abstractions.Messaging;

namespace Nexus.UserManagement.Application.Features.Countries.Commands.Create
{
    public sealed record CreateCountryCommand(string Name) : IRequest<Result<Guid>>, ICommand;
}