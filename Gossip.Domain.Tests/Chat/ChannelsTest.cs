using System.Collections.Generic;
using Gossip.Domain.Models.Chat;
using Gossip.Domain.Repositories.Chat;
using Moq;
using Xunit;

namespace Gossip.Domain.Tests.Chat
{
    public class ChannelsTest
    {
        [Fact]
        public void AddAndVerifyChannels()
        {
            // Arrange
            var mock = new Mock<IChannelRepository>();
            mock.Setup(repo => repo.GetAll()).Returns(new List<Channel>());
            
            // Act


            // Assert
        }
    }
}
