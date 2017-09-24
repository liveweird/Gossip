using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gossip.Contract.Interfaces.Chat;
using Gossip.Web.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;
using LanguageExt;

namespace Gossip.Web.Controllers.Dashboard
{
    [Route("api/dashboard/[controller]")]
    public class MessagesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IChatService _chatService;

        public MessagesController(IMapper mapper, IChatService chatService)
        {
            _mapper = mapper;
            _chatService = chatService;
        }

        [HttpGet("getAllByChannel/{channelId}")]
        public async Task<IActionResult> Get(int channelId)
        {
            TryAsync<Lst<Contract.DTO.Chat.Message>> svcResult = async () => {
                return await _chatService.GetAllMessagesInChannel(channelId);
            };

            var result = svcResult.Match<Lst<Contract.DTO.Chat.Message>, IActionResult>(
                Succ: model => Ok(model.Select(p => p.Content)),
                Fail: ex => StatusCode(500, ex)
            );

            return await result;
        }

        [HttpPost("addInChannel/{channelId}")]
        public void Post([FromBody]Message message, int channelId)
        {
            message.ChannelId = channelId;
            var model = _mapper.Map<Message, Contract.DTO.Chat.Message>(message);
            _chatService.AddMessage(model);
        }
    }
}