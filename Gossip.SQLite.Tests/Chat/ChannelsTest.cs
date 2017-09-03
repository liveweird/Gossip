using System;
using Gossip.Domain.Models.Chat;
using Gossip.SQLite.Repositories.Chat;
using Xunit;

namespace Gossip.SQLite.Tests.Chat
{
    public class ChannelsTest
    {
        [Fact]
        public void AddAndVerifyChannels()
        {
            // Arrange
            using (var ctx = new GossipContext())
            {
                var channelRepo = new ChannelRepository(ctx);
                ctx.Channels.RemoveRange(ctx.Channels);

                // Act
                channelRepo.Insert(new Channel {Name = "abc", Description = "def"});
                var channels = channelRepo.GetAll();

                // Assert
                Assert.Collection(channels, p =>
                {
                    Assert.Equal(p.Name, "abc");
                    Assert.Equal(p.Description, "def");
                });
            }
        }
    }
}
