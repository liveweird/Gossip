using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gossip.Contract.Interfaces.Chat;
using Gossip.Web.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace Gossip.Web.Controllers.Dashboard
{
    [Route("api/dashboard/channels/{channelId}/[controller]")]
    public class MessagesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IChatService _chatService;

        public MessagesController(IMapper mapper, IChatService chatService)
        {
            _mapper = mapper;
            _chatService = chatService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get(int channelId)
        {
            return (await _chatService.GetAllMessagesInChannel(channelId)).Select(c => c.Content);
        }

        [HttpGet("{id}")]
        public string Get(int id, int channelId)
        {
            return "message";
        }

        [HttpPost]
        public void Post([FromBody]Message message, int channelId)
        {
            message.ChannelId = channelId;
            var model = _mapper.Map<Message, Contract.DTO.Chat.Message>(message);
            _chatService.AddMessage(model);
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