using FluentValidation;
using Nexus.UserManagement.Application.Abstractions.Validators;

namespace Nexus.UserManagement.Application.Validators
{
    public sealed class CodeValidator : AbstractValidator<IHasCode>
    {
        public static CodeValidator Create() => new();

        public CodeValidator()
        {
            RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Пустой код подтверждения")
            .Length(6, 6).WithMessage("Код должен быть ровно 6 символов");
        }
    }
}