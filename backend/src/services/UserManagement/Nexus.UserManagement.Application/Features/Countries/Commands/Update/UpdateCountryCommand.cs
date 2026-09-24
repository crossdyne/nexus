using MediatR;
using Crossdyne.Toolkit.Results;
using Nexus.UserManagement.Application.Abstractions.Messaging;

namespace Nexus.UserManagement.Application.Features.Countries.Commands.Update
{
    public sealed record UpdateCountryCommand(Guid Id, string Name) : IRequest<Result>, ICommand;
}