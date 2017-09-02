using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Gossip.Web.Controllers
{
    [Route("api/[controller]")]
    public class ChannelsController : Controller
    {
        // GET api/channels
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "channel1", "channel2" };
        }

        // GET api/channels/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "channel";
        }

        // POST api/channels
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
