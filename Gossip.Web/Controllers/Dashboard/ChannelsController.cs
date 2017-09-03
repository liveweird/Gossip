using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Gossip.Application.Contracts.Chat;
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

        // GET api/channels
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _chatService.GetAllChannels().Select(p => p.Name);
        }

        // GET api/channels/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "channel";
        }

        // POST api/channels
        [HttpPost]
        public void Post([FromBody]Channel channel)
        {
            var model = _mapper.Map<Channel, Application.Models.Chat.Channel>(channel);
            _chatService.AddChannel(model);
        }

        // PUT api/channels/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/channels/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
