using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gossip.Contract;
using Gossip.Contract.Interfaces.Chat;
using Gossip.Domain.Repositories.Chat;
using Channel = Gossip.Contract.DTO.Chat.Channel;
using DomainChannel = Gossip.Domain.Models.Chat.Channel;
using Message = Gossip.Contract.DTO.Chat.Message;
using DomainMessage = Gossip.Domain.Models.Chat.Message;
using Gossip.Domain.Repositories;
using LanguageExt;

namespace Gossip.Application.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly IMapper _mapper;
        private readonly IChannelRepository _channelRepository;
        private readonly IUnitOfWorkFactory _uowFactory;

        public ChatService(IMapper mapper, IChannelRepository channelRepository, IUnitOfWorkFactory uowFactory)
        {
            _mapper = mapper;
            _channelRepository = channelRepository;
            _uowFactory = uowFactory;
        }

        public TryAsync<Unit> AddChannel(Channel channel)
        {
            return async () =>
            {
                using (var uow = await _uowFactory.CreateAsync())
                {
                    var toInsert = _mapper.Map<Channel, DomainChannel>(channel);
                    _channelRepository.InsertChannel(toInsert);
                    await uow.CommitChangesAsync();
                    return Unit.Default;
                }
            };            
        }

        public async Task<IEnumerable<Channel>> GetAllChannels()
        {
            var channels = await _channelRepository.GetAllChannels();
            return _mapper.Map<IEnumerable<DomainChannel>, IEnumerable<Channel>>(channels);
        }

        public async Task<IEnumerable<Message>> GetAllMessagesInChannel(int channelId)
        {
            var channel = await _channelRepository.GetAsync(channelId) ?? throw new ArgumentException("Provide channel identifier is not proper!");
            return _mapper.Map<IEnumerable<DomainMessage>, IEnumerable<Message>>(channel.Messages.ToList());
        }

        public async Task AddMessage(Message message)
        {
            using (var uow = await _uowFactory.CreateAsync())
            {
                var channel = await _channelRepository.GetAsync(message.ChannelId);
                channel.AddMessage(message.Content, null);
                _channelRepository.UpdateChannel(channel);
                await uow.CommitChangesAsync();
            }
        }
    }
}
