using FluentValidation;
using Nexus.UserManagement.Application.Validators;
using Shared.Validations.Validators;

namespace Nexus.UserManagement.Application.Features.Users.Commands.ResetPasswordConfirmCode
{
    public sealed class ResetPasswordConfirmCodeCommandValidator : AbstractValidator<ResetPasswordConfirmCodeCommand>
    {
        public ResetPasswordConfirmCodeCommandValidator()
        {
            Include(LoginValidator.Create());
            Include(CodeValidator.Create());
        }
    }
}