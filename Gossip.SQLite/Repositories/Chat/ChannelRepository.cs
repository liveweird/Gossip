using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gossip.Domain.Models.Chat;
using Gossip.Domain.Repositories.Chat;
using Microsoft.EntityFrameworkCore;

namespace Gossip.SQLite.Repositories.Chat
{
    public class ChannelRepository : Repository<Channel>, IChannelRepository
    {
        public ChannelRepository(GossipContext context) : base(context)
        {
        }

        public override async Task<Channel> GetAsync(int id)
        {
            var channel = await Context.Channels.FindAsync(id);
            if (channel != null)
            {
                await Context.Entry(channel)
                    .Collection(i => i.Messages).LoadAsync();
            }

            return channel;
        }

        public Channel InsertChannel(Channel channel)
        {
            if (channel == null)
            {
                throw new ArgumentNullException(nameof(channel));
            }

            return Context.Channels.Add(channel).Entity;
        }

        public void UpdateChannel(Channel channel)
        {
            if (channel == null)
            {
                throw new ArgumentNullException(nameof(channel));
            }

            Context.Entry(channel).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Channel>> GetAllChannels()
        {
            var channels = Context.Channels;

            await channels.ForEachAsync(async channel =>
             {
                 await Context.Entry(channel).Collection(i => i.Messages).LoadAsync();
             });

            return channels;
        }
    }
}