using AutoMapper;
using Gossip.Web.ViewModels.Dashboard;

namespace Gossip.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Application.Models.Chat.Channel, Domain.Models.Chat.Channel>();
            CreateMap<Domain.Models.Chat.Channel, Application.Models.Chat.Channel>();

            CreateMap<Application.Models.Chat.Message, Domain.Models.Chat.Message>();
            CreateMap<Domain.Models.Chat.Message, Application.Models.Chat.Message>();

            CreateMap<Application.Models.Chat.Channel, Channel>();
            CreateMap<Channel, Application.Models.Chat.Channel>();

            CreateMap<Application.Models.Chat.Message, Message>();
            CreateMap<Message, Application.Models.Chat.Message>();
        }
    }
}
