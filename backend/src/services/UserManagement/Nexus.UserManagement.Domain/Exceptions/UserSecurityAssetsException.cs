using Crossdyne.Toolkit.Results;
using Shared.Kernel.Exceptions;

namespace Nexus.UserManagement.Domain.Exceptions
{
    public sealed class UserSecurityAssetsException(Error error) : DomainException(error)
    {
    }
}