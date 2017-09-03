using System.Collections.Generic;
using AutoMapper;
using Gossip.Application.Contracts.Chat;
using Gossip.Domain.Events.Chat;
using Gossip.Domain.Repositories.Chat;
using MediatR;
using Channel = Gossip.Application.Models.Chat.Channel;
using DomainChannel = Gossip.Domain.Models.Chat.Channel;

namespace Gossip.Application.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IChannelRepository _channelRepository;

        public ChatService(IMapper mapper, IMediator mediator, IChannelRepository channelRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _channelRepository = channelRepository;
        }

        public async void AddChannel(Channel channel)
        {
            await _mediator.Publish(new NewChannelSubmittedEvent
            {
                Name = channel.Name,
                Description = channel.Description
            });
        }

        public IEnumerable<Channel> GetAllChannels()
        {
            var channels = _channelRepository.GetAll();
            return _mapper.Map<IEnumerable<DomainChannel>, IEnumerable<Channel>>(channels);
        }
    }
}
