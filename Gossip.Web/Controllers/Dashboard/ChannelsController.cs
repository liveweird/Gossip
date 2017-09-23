using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gossip.Contract.Interfaces.Chat;
using Gossip.Web.ViewModels.Dashboard;
using LanguageExt;
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

        [HttpGet("getAll")]
        public async Task<IActionResult> Get()
        {
            var model = await _chatService.GetAllChannels();
            return Ok(model.Select(p => p.Name));
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok("channel");
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody]Channel channel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<Channel, Contract.DTO.Chat.Channel>(channel);
            var svcResult = _chatService.AddChannel(model);

            var result = svcResult.Match<Unit, IActionResult>(
                Succ: unit => NoContent(),
                Fail: ex => StatusCode(500, ex)
            );

            return await result;
        }
    }
}
