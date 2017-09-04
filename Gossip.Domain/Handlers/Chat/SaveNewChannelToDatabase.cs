using Gossip.Domain.Events.Chat;
using Gossip.Domain.Models.Chat;
using Gossip.Domain.Repositories.Chat;
using MediatR;

namespace Gossip.Domain.Handlers.Chat
{
    public class SaveNewChannelToDatabase : INotificationHandler<NewChannelSubmittedEvent>
    {
        private readonly IChannelRepository _channelRepository;

        public SaveNewChannelToDatabase(IChannelRepository repository)
        {
            _channelRepository = repository;
        }

        public void Handle(NewChannelSubmittedEvent notification)
        {
            _channelRepository.Insert(new Channel { Name = notification.Name, Description = notification.Description });
        }
    }
}
