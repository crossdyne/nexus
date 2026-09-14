using MediatR;
using Shared.Kernel.Primitives;

namespace Nexus.UserManagement.Application.Events
{
    public sealed record DomainEventNotification<TDomainEvent>(TDomainEvent DomainEvent) : INotification where TDomainEvent : IDomainEvent;
}