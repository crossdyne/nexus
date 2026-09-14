using Nexus.Authentication.Domain.Models;
using Shared.Kernel.Interfaces;

namespace Nexus.Authentication.Application.Abstractions.Repositories
{
    public interface IAccessDataRepository : IRepository<AccessData>
    {
        Task<int> CloseSessions(Guid userId, DateTime eventDateTime);
    }
}