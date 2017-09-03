using System.Collections.Generic;
using System.Linq;
using Gossip.Application;
using Gossip.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gossip.Web.Controllers
{
    [Route("api/[controller]")]
    public class ChannelsController : Controller
    {
        private readonly IChatService _chatService;

        public ChannelsController(IChatService chatService)
        {
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
            _chatService.AddChannel(channel.Name, channel.Description);
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
