using FluentValidation;
using Nexus.UserManagement.Application.Validators;
using Shared.Validations.Validators;

namespace Nexus.UserManagement.Application.Features.Users.Commands.Register
{
    public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            Include(LoginValidator.Create());
            Include(UserNameValidator.Create());
            Include(EncryptedVerifierValidator.Create());
            Include(SrpSaltValidator.Create());
            Include(EncryptedDekValidator.Create());
        }
    }
}