using FluentValidation;
using Nexus.UserManagement.Application.Abstractions.Validators;

namespace Nexus.UserManagement.Application.Validators
{
    public sealed class EncryptedDekValidator : AbstractValidator<IHasEncryptedDek>
    {
        public static EncryptedDekValidator Create() => new();

        public EncryptedDekValidator()
        {
            RuleFor(x => x.EncryptedVerifierWrapKey)
            .NotEmpty().WithMessage("Ключ DEK не может быть пустым.");
        }    
    }
}