using AutoMapper;

namespace Gossip.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Application.Models.Channel, Domain.Models.Channel>();
            CreateMap<Domain.Models.Channel, Application.Models.Channel>();

            CreateMap<Application.Models.Channel, Models.Channel>();
            CreateMap<Models.Channel, Application.Models.Channel>();
        }
    }
}
