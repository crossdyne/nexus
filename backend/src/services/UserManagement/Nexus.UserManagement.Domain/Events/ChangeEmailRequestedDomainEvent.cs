using Nexus.UserManagement.Domain.ValueObjects.User;
using Shared.Kernel.Primitives;

namespace Nexus.UserManagement.Domain.Events
{
    public sealed record ChangeEmailRequestedDomainEvent(Guid IdEvent, DateTime OccurredOnUtc, UserId UserId, Email Email, string Code, DateTime ExpiresAt) : IDomainEvent;
}