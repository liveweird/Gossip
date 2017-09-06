using Gossip.Domain.Events.Chat;
using Gossip.Domain.External.BlobStorage;
using Gossip.Domain.Models.Chat;
using MediatR;

namespace Gossip.Application.Handlers.Chat
{
    public class DumpNewMessageToBlobStorage : INotificationHandler<NewMessageCreatedEvent>
    {
        private readonly IBlobStorage _blobStorage;

        public DumpNewMessageToBlobStorage(IBlobStorage blobStorage)
        {
            _blobStorage = blobStorage;
        }

        public void Handle(NewMessageCreatedEvent notification)
        {
            _blobStorage.DoSomething(new Message { Content = notification.Content });
        }
    }
}