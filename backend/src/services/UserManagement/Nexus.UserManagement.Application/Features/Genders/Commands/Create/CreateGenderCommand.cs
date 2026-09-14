using MediatR;
using Crossdyne.Toolkit.Results;
using Nexus.UserManagement.Application.Abstractions.Messaging;

namespace Nexus.UserManagement.Application.Features.Genders.Commands.Create
{
    public sealed record CreateGenderCommand(string Name) : IRequest<Result<Guid>>, ICommand;
}