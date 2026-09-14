using System.Threading.Channels;

namespace Nexus.UserManagement.Application.Abstractions.Outbox
{
    public interface IOutboxSignal
    {
        void Signal();
        ChannelReader<bool> Reader { get; }
    }
}