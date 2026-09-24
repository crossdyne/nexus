using FluentValidation;
using Nexus.UserManagement.Application.Validators;
using Shared.Validations.Validators;

namespace Nexus.UserManagement.Application.Features.Users.Commands.RecoveryViaKeys
{
    public sealed class RecoveryViaKeysCommandValidator : AbstractValidator<RecoveryViaKeysCommand>
    {
        public RecoveryViaKeysCommandValidator()
        {
            Include(LoginValidator.Create());
            Include(EncryptedVerifierValidator.Create());
            Include(SrpSaltValidator.Create());
            Include(EncryptedDekValidator.Create());
        }
    }
}