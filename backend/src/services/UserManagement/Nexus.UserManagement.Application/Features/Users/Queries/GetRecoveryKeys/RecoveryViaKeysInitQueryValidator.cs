using FluentValidation;
using Shared.Validations.Validators;

namespace Nexus.UserManagement.Application.Features.Users.Queries.GetRecoveryKeys
{
    public sealed class GetRecoveryKeysQueryValidator : AbstractValidator<GetRecoveryKeysQuery>
    {
        public GetRecoveryKeysQueryValidator()
        {
            Include(new LoginValidator());
        }
    }
}