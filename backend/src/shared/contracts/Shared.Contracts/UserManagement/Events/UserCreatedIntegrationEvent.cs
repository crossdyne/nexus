using Shared.Abstractions.Messaging;

namespace Shared.Contracts.UserManagement.Events
{
    public sealed record UserCreatedIntegrationEvent(Guid IdEvent, DateTime OccurredOnUtc, Guid UserId, string UserName) : IIntegrationEvent;
}