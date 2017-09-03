using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gossip.Web.Models.Dashboard;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace Gossip.Web.Tests.Dashboard
{
    public class ChannelsTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ChannelsTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        private StringContent CreateNewChannelContent(string name, string description)
        {
            return new StringContent(JsonConvert.SerializeObject(new Channel { Name = name, Description = description }),
                Encoding.UTF8,
                "application/json");
        }

        [Fact]
        public async Task ProperChannelListIsReturned()
        {
            // Act
            var content1 = CreateNewChannelContent("channel1", "abc");
            var response1 =
                await _client.PostAsync("/api/dashboard/channels", content1);
            response1.EnsureSuccessStatusCode();

            var content2 = CreateNewChannelContent("channel2", "abc");
            var response2 =
                await _client.PostAsync("/api/dashboard/channels", content2);
            response2.EnsureSuccessStatusCode();

            var response3 = await _client.GetAsync("/api/dashboard/channels");
            response3.EnsureSuccessStatusCode();

            var responseString = await response3.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("[\"channel1\",\"channel2\"]",
                responseString);
        }
    }
}
