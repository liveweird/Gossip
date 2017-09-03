using System.Collections.Generic;
using Gossip.Domain;
using Gossip.Domain.Models;

namespace Gossip.Application
{
    public class ChatService : IChatService
    {
        private readonly IChannelRepository _channelRepository;

        public ChatService(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public void AddChannel(string name, string description)
        {
            _channelRepository.Insert(new Channel { Name = name, Description = description });
        }

        public IEnumerable<Channel> GetAllChannels()
        {
            return _channelRepository.GetAll();
        }
    }
}
