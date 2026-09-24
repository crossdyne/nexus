using Crossdyne.Toolkit.Results;
using Shared.Kernel.Exceptions;

namespace Nexus.UserManagement.Domain.Exceptions
{
    public sealed class EmptyValueException(Error error) : DomainException(error)
    {
    }
}