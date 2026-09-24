using MediatR;
using Crossdyne.Toolkit.Results;
using Shared.Abstractions.Validations;
using Nexus.UserManagement.Application.Abstractions.Messaging;
using Nexus.UserManagement.Application.Abstractions.Validators;

namespace Nexus.UserManagement.Application.Features.Users.Commands.ResetPasswordConfirmCode
{
    public sealed record ResetPasswordConfirmCodeCommand(string Login, string Code) : IRequest<Result>, ICommand, IHasLogin, IHasCode;
}