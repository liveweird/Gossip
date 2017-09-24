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
            TryAsync<Lst<Contract.DTO.Chat.Channel>> svcResult = async () => await _chatService.GetAllChannels();

            var result = svcResult.Match<Lst<Contract.DTO.Chat.Channel>, IActionResult>(
                Succ: model => Ok(model.Select(p => p.Name)),
                Fail: ex => StatusCode(500, ex)
            );

            return await result;
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok("channel");
        }

        [HttpPost("add")]
        [ValidationFilter]
        public async Task<IActionResult> Post([FromBody]Channel channel)
        {
            var model = _mapper.Map<Channel, Contract.DTO.Chat.Channel>(channel);
            TryAsync<Unit> svcResult = async () => await _chatService.AddChannel(model);

            var result = svcResult.Match<Unit, IActionResult>(
                Succ: unit => NoContent(),
                Fail: ex => StatusCode(500, ex)
            );

            return await result;
        }
    }
}
