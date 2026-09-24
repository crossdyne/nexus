using Nexus.UserManagement.Domain.Enums;
using Nexus.UserManagement.Domain.ValueObjects.User;
using Nexus.UserManagement.Domain.ValueObjects.UserAuthenticator;
using Shared.Kernel.Primitives;

namespace Nexus.UserManagement.Domain.Models
{
    public abstract class UserAuthenticator : Entity<UserAuthenticatorId>
    {
        public UserId UserId { get; private set; }
        public UserAuthenticatorType Method { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? LastUsedAt { get; protected set; } 
        public bool IsActive { get; protected set; } = true;

        protected UserAuthenticator()
        {
            
        }

        protected UserAuthenticator(UserAuthenticatorId id, UserId userId, UserAuthenticatorType method) : base(id)
        {
            UserId = userId;
            Method = method;
        }

        internal void MarkUsed() => LastUsedAt = DateTime.UtcNow;
        internal void Activate() => IsActive = true;
        internal void Deactivate() => IsActive = false;
    }
}