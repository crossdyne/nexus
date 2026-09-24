using FluentValidation;
using Nexus.UserManagement.Application.Abstractions.Validators;

namespace Nexus.UserManagement.Application.Validators
{
    public sealed class EncryptedVerifierValidator : AbstractValidator<IHasEncryptedVerifier>
    {
        public static EncryptedVerifierValidator Create() => new();
        
        public EncryptedVerifierValidator()
        {
            RuleFor(x => x.EncryptedVerifier)
            .NotEmpty().WithMessage("Верификатор не может быть пустым.");
        }
    }
}