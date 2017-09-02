using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Gossip.Web.Tests
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

        [Fact]
        public async Task ProperChannelListIsReturned()
        {
            // Act
            var response = await _client.GetAsync("/api/channels");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("[\"channel1\",\"channel2\"]",
                responseString);
        }
    }
}
