using System.Collections.Generic;
using System.Threading.Tasks;
using Gossip.Contract.DTO.Chat;
using LanguageExt;
using Channel = Gossip.Contract.DTO.Chat.Channel;

namespace Gossip.Contract.Interfaces.Chat
{
    public interface IChatService
    {
        Task<Unit> AddChannel(Channel channel);
        Task<Unit> AddMessage(Message message);
        Task<Lst<Channel>> GetAllChannels();
        Task<Lst<Message>> GetAllMessagesInChannel(int channelId);
    }
}