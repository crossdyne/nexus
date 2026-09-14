using Shared.Kernel.Primitives;

namespace Nexus.UserManagement.Application.Abstractions.Outbox
{
    public interface IDbContextOutbox
    {
        void Append(IReadOnlyCollection<IDomainEvent> domainEvents);
    }
}