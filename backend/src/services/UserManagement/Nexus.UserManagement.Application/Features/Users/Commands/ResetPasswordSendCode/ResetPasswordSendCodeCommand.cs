using MediatR;
using Crossdyne.Toolkit.Results;
using Shared.Abstractions.Validations;
using Nexus.UserManagement.Application.Abstractions.Messaging;

namespace Nexus.UserManagement.Application.Features.Users.Commands.ResetPasswordSendCode
{
    public sealed record ResetPasswordSendCodeCommand(string Login) : IRequest<Result>, ICommand, IHasLogin;
}