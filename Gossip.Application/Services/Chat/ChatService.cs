using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Gossip.Application.Contracts.Chat;
using Gossip.Domain.Repositories.Chat;
using Channel = Gossip.Application.Models.Chat.Channel;
using DomainChannel = Gossip.Domain.Models.Chat.Channel;

namespace Gossip.Application.Services.Chat
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

        public async Task<bool> AddChannel(Channel channel)
        {
            var toInsert = _mapper.Map<Channel, DomainChannel>(channel);
            _channelRepository.Insert(toInsert);
            return await _channelRepository.UnitOfWork.SaveEntitiesAsync();
        }

        public IEnumerable<Channel> GetAllChannels()
        {
            var channels = _channelRepository.GetAll();
            return _mapper.Map<IEnumerable<DomainChannel>, IEnumerable<Channel>>(channels);
        }

        public async Task<bool> AddMessage(int channelId, int? parentId, string content)
        {
            var channel = _channelRepository.Get(channelId);
            channel.AddMessage(parentId, content);
            return await _channelRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
