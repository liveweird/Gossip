using AutoMapper;
using Gossip.Web.ViewModels.Dashboard;

namespace Gossip.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contract.DTO.Chat.Channel, Domain.Models.Chat.Channel>();
            CreateMap<Domain.Models.Chat.Channel, Contract.DTO.Chat.Channel>();

            CreateMap<Contract.DTO.Chat.Message, Domain.Models.Chat.Message>();
            CreateMap<Domain.Models.Chat.Message, Contract.DTO.Chat.Message>();

            CreateMap<Contract.DTO.Chat.Channel, Channel>();
            CreateMap<Channel, Contract.DTO.Chat.Channel>();

            CreateMap<Contract.DTO.Chat.Message, Message>();
            CreateMap<Message, Contract.DTO.Chat.Message>();
        }
    }
}
