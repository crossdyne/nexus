using Nexus.UserManagement.Domain.ValueObjects.User;
using Shared.Kernel.Primitives;

namespace Nexus.UserManagement.Domain.Events
{
    public sealed record UserCreatedDomainEvent(
        Guid IdEvent,
        DateTime OccurredOnUtc,
        UserId UserId,
        UserName UserName) : IDomainEvent;
}