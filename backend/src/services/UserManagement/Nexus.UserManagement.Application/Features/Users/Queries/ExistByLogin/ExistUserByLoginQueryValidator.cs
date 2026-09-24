using FluentValidation;
using Shared.Validations.Validators;

namespace Nexus.UserManagement.Application.Features.Users.Queries.ExistByLogin
{
    public sealed class ExistUserByLoginQueryValidator : AbstractValidator<ExistUserByLoginQuery>
    {
        public ExistUserByLoginQueryValidator()
        {
            Include(new LoginValidator());
        }
    }
}