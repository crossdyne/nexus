using FluentValidation;
using Nexus.Authentication.Application.Validators;
using Shared.Validations.Validators;

namespace Nexus.Authentication.Application.Features.Commands.VerifySrpProof
{
    public sealed class VerifySrpProofCommandValidator : AbstractValidator<VerifySrpProofCommand>
    {
        public VerifySrpProofCommandValidator()
        {
            Include(LoginValidator.Create());
            Include(SrpProofValidator.Create());
        }
    }
}