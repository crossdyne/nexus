using Nexus.UserManagement.Domain.Enums;
using Nexus.UserManagement.Domain.ValueObjects.User;
using Nexus.UserManagement.Domain.ValueObjects.UserAuthenticator;

namespace Nexus.UserManagement.Domain.Models
{
    public sealed class EmailAuthenticator : UserAuthenticator
    {
        public Email? Email { get; private set; }

        private EmailAuthenticator()
        {
            
        }

        private EmailAuthenticator(UserId userId, Email email) : base(UserAuthenticatorId.New(), userId, UserAuthenticatorType.Email)
        {
            Email = email;
        }

        internal static EmailAuthenticator Create(UserId userId, Email email)
        {
            return new EmailAuthenticator(userId, email);
        }

        internal void Update(Email email)
        {
            Email = email;
        }
    }
}