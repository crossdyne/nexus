using MediatR;
using Crossdyne.Toolkit.Results;
using Nexus.UserManagement.Application.Abstractions.Messaging;

namespace Nexus.UserManagement.Application.Features.Genders.Commands.Delete
{
    public sealed record DeleteGenderCommand(Guid Id) : IRequest<Result>, ICommand;
}