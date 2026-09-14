using FluentValidation;
using Nexus.UserManagement.Application.Validators;

namespace Nexus.UserManagement.Application.Features.Users.Commands.ChangeUserName
{
    public sealed class ChangeUserNameCommandValidator : AbstractValidator<ChangeUserNameCommand>
    {
        public ChangeUserNameCommandValidator()
        {
            Include(new UserNameValidator());
        }
    }
}