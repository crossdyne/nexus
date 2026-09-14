using Crossdyne.Toolkit.Results;
using MediatR;
using Unit = Crossdyne.Toolkit.Primitives.Unit;

namespace Nexus.Authentication.Application.Features.Commands.Logout
{
    public sealed record LogoutCommand(string RefreshToken) : IRequest<Result<Unit>>;
}