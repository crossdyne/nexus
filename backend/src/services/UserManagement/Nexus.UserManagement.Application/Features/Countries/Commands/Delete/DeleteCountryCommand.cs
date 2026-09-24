using MediatR;
using Crossdyne.Toolkit.Results;
using Nexus.UserManagement.Application.Abstractions.Messaging;

namespace Nexus.UserManagement.Application.Features.Countries.Commands.Delete
{
    public sealed record DeleteCountryCommand(Guid Id) : IRequest<Result>, ICommand;
}