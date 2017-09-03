using System.Collections.Generic;
using AutoMapper;
using Gossip.Domain;
using Channel = Gossip.Application.Models.Channel;
using DomainChannel = Gossip.Domain.Models.Channel;

namespace Gossip.Application
{
    public class ChatService : IChatService
    {
        private readonly IMapper _mapper;
        private readonly IChannelRepository _channelRepository;

        public ChatService(IMapper mapper, IChannelRepository channelRepository)
        {
            _mapper = mapper;
            _channelRepository = channelRepository;
        }

        public void AddChannel(Channel channel)
        {
            var model = _mapper.Map<Channel, DomainChannel>(channel);
            _channelRepository.Insert(model);
        }

        public IEnumerable<Channel> GetAllChannels()
        {
            var channels = _channelRepository.GetAll();
            return _mapper.Map<IEnumerable<DomainChannel>, IEnumerable<Channel>>(channels);
        }
    }
}
