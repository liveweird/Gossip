using System.Net;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using Gossip.Web.Controllers.Dashboard;
using Gossip.Web.ViewModels.Dashboard;
using Machine.Specifications;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace Gossip.Web.Tests.Dashboard
{
    internal class ApiTestHelper
    {
        internal static (TestServer, HttpClient) BuildContext()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            var client = server.CreateClient();
            return (server, client);
        }
    }

    internal class ChannelTestHelper
    {
        internal static StringContent CreateNewChannelContent(string name, string description)
        {
            return new StringContent(JsonConvert.SerializeObject(new Channel { Name = name, Description = description }),
                Encoding.UTF8,
                "application/json");
        }

        internal static StringContent CreateNewMessageContent(int? parentId, string content)
        {
            return new StringContent(JsonConvert.SerializeObject(new Message { ParentId = parentId, Content = content }),
                Encoding.UTF8,
                "application/json");
        }
    }

    [Tags("Scenario")]
    [Subject(typeof(ChannelsController), "Add Two Channels")]
    public class When_adding_two_channels
    {
        static TestServer Server;
        static HttpClient Client;
        static HttpResponseMessage Response3;

        Establish context = () =>
        {
            (Server, Client) = ApiTestHelper.BuildContext();
        };

        Because of = () =>
        {
            var content1 = ChannelTestHelper.CreateNewChannelContent("channelA", "abc");
            var response1 =
                Client.PostAsync("/api/dashboard/channels/add", content1).Result;
            response1.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var content2 = ChannelTestHelper.CreateNewChannelContent("channelB", "abc");
            var response2 =
                Client.PostAsync("/api/dashboard/channels/add", content2).Result;
            response2.StatusCode.Should().Be(HttpStatusCode.NoContent);

            Response3 = Client.GetAsync("/api/dashboard/channels/getAll").Await();
        };

        It should_return_a_successful_code = () =>
        {
            Response3.StatusCode.Should().Be(HttpStatusCode.OK);
        };

        It should_return_both_added_channels = async () =>
        {
            var responseString = await Response3.Content.ReadAsStringAsync();
            responseString.Should().Be("[\"channelA\",\"channelB\"]");
        };
    }

    [Tags("Scenario")]
    [Subject(typeof(ChannelsController), "Add channels with empty name")]
    public class When_adding_a_channel_with_empty_name
    {
        static TestServer Server;
        static HttpClient Client;
        static HttpResponseMessage Response1;

        Establish context = () =>
        {
            (Server, Client) = ApiTestHelper.BuildContext();
        };

        Because of = () =>
        {
            var content1 = ChannelTestHelper.CreateNewChannelContent("", "abc");
            Response1 =
                Client.PostAsync("/api/dashboard/channels/add", content1).Await();
        };

        It should_raise_an_error = () =>
        {
            Response1.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        };

        It should_provide_an_explanation = async () =>
        {
            var response1String = await Response1.Content.ReadAsStringAsync();
            response1String.Should().Be("{\"Name\":[\"\'Name\' should not be empty.\"]}");
        };
    }

    [Tags("Scenario")]
    [Subject(typeof(ChannelsController), "Add message to channel")]
    public class When_adding_a_message_to_channel
    {
        static TestServer Server;
        static HttpClient Client;
        static HttpResponseMessage Response3;

        Establish context = () =>
        {
            (Server, Client) = ApiTestHelper.BuildContext();
        };

        private Because of = () =>
        {
            var channelId = 1;

            var content1 = ChannelTestHelper.CreateNewChannelContent("channelA", "abc");
            var response1 =
                Client.PostAsync("/api/dashboard/channels/add", content1).Result;
            response1.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var content2 = ChannelTestHelper.CreateNewMessageContent(null, "def");
            var response2 =
                Client.PostAsync($"/api/dashboard/messages/addInChannel/{channelId}", content2).Result;
            response2.StatusCode.Should().Be(HttpStatusCode.NoContent);

            Response3 = Client.GetAsync($"/api/dashboard/messages/getAllByChannel/{channelId}").Await();
        };

        It should_return_a_successful_code = () =>
        {
            Response3.StatusCode.Should().Be(HttpStatusCode.OK);
        };

        It should_return_the_message_created = async () =>
        {
            var responseString = await Response3.Content.ReadAsStringAsync();
            responseString.Should().Be("[\"def\"]");
        };
    }

    [Tags("Scenario")]
    [Subject(typeof(ChannelsController), "No message in non-existent channel")]
    public class When_getting_messages_out_of_nonexistent_channel
    {
        static TestServer Server;
        static HttpClient Client;
        static HttpResponseMessage Response1;

        Establish context = () =>
        {
            (Server, Client) = ApiTestHelper.BuildContext();
        };

        Because of = () =>
        {
            var channelId = 44;
            Response1 = Client.GetAsync($"/api/dashboard/messages/getAllByChannel/{channelId}").Await();
        };

        It should_raise_an_error = () =>
        {
            Response1.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        };

        It should_provide_an_explanation = async () =>
        {
            var response1String = await Response1.Content.ReadAsStringAsync();
            response1String.Should().StartWith("{\"ClassName\":\"System.ArgumentException\"");
        };
    }
}
