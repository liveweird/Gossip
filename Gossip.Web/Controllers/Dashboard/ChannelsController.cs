using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gossip.Contract.Chat;
using Gossip.Web.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace Gossip.Web.Controllers.Dashboard
{
    [Route("api/dashboard/[controller]")]
    public class ChannelsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IChatService _chatService;

        public ChannelsController(IMapper mapper, IChatService chatService)
        {
            _mapper = mapper;
            _chatService = chatService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return (await _chatService.GetAllChannels()).Select(p => p.Name);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "channel";
        }

        [HttpPost]
        public void Post([FromBody]Channel channel)
        {
            var model = _mapper.Map<Channel, Contract.DTO.Chat.Channel>(channel);
            _chatService.AddChannel(model);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
