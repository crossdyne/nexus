using Nexus.UserManagement.Domain.ValueObjects.User;
using Shared.Kernel.Primitives;

namespace Nexus.UserManagement.Domain.Events
{
    public sealed record PasswordResetRequestedDomainEvent(
        Guid IdEvent,
        DateTime OccurredOnUtc,
        UserId UserId,
        string Email,
        string Code,
        DateTime ExpiresAt) : IDomainEvent;
}