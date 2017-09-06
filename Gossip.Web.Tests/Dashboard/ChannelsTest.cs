using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gossip.Web.ViewModels.Dashboard;
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

        private StringContent CreateNewMessageContent(int? parentId, string content)
        {
            return new StringContent(JsonConvert.SerializeObject(new Message { ParentId = parentId, Content = content }),
                Encoding.UTF8,
                "application/json");
        }

        [Fact]
        public async Task AddTwoChannels()
        {
            // Act
            var content1 = CreateNewChannelContent("channelA", "abc");
            var response1 =
                await _client.PostAsync("/api/dashboard/channels", content1);
            response1.EnsureSuccessStatusCode();

            var content2 = CreateNewChannelContent("channelB", "abc");
            var response2 =
                await _client.PostAsync("/api/dashboard/channels", content2);
            response2.EnsureSuccessStatusCode();

            var response3 = await _client.GetAsync("/api/dashboard/channels");
            response3.EnsureSuccessStatusCode();

            var responseString = await response3.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("[\"channelA\",\"channelB\"]",
                responseString);
        }

        [Fact]
        public async Task AddMessageToChannel()
        {
            // Act
            var content1 = CreateNewChannelContent("channelA", "abc");
            var response1 =
                await _client.PostAsync("/api/dashboard/channels", content1);
            response1.EnsureSuccessStatusCode();

            var content2 = CreateNewMessageContent(null, "def");
            var channelId = 1;
            var response2 =
                await _client.PostAsync($"/api/dashboard/channels/{channelId}/messages", content2);
            response2.EnsureSuccessStatusCode();

            var response3 = await _client.GetAsync($"/api/dashboard/channels/{channelId}/messages");
            response3.EnsureSuccessStatusCode();

            var responseString = await response3.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("[\"def\"]",
                responseString);
        }
    }
}
