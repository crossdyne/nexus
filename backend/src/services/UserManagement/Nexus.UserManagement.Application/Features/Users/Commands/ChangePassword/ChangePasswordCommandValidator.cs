using FluentValidation;
using Nexus.UserManagement.Application.Validators;
using Shared.Validations.Validators;

namespace Nexus.UserManagement.Application.Features.Users.Commands.ChangePassword
{
    public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            Include(GuidUserIdValidator.Create());
            Include(EncryptedVerifierValidator.Create());
            Include(SrpSaltValidator.Create());
            Include(EncryptedDekValidator.Create());
        }    
    }
}