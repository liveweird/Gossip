using Gossip.Domain.Events.Chat;
using Gossip.Domain.External.GraphDataDump;
using Gossip.Domain.Models.Chat;
using MediatR;

namespace Gossip.Domain.Handlers.Chat
{
    public class DumpNewChannelToBlobStorage : INotificationHandler<NewChannelSubmittedEvent>
    {
        private readonly IBlobStorage _blobStorage;

        public DumpNewChannelToBlobStorage(IBlobStorage blobStorage)
        {
            _blobStorage = blobStorage;
        }

        public void Handle(NewChannelSubmittedEvent notification)
        {
            _blobStorage.DoSomething(new Channel { Name = notification.Name, Description = notification.Description });
        }
    }
}