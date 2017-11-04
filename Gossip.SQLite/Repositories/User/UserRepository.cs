using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gossip.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace Gossip.SQLite.Repositories.User
{
    public class UserRepository : Repository<Domain.Models.User.User>, IUserRepository
    {
        public UserRepository(GossipContext context) : base(context)
        {
        }

        public override async Task<Domain.Models.User.User> GetAsync(int id)
        {
            var user = await Context.Users.FindAsync(id);
            return user;
        }

        public Domain.Models.User.User InsertUser(Domain.Models.User.User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Context.Users.Add(user).Entity;
        }

        public void UpdateUser(Domain.Models.User.User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Context.Entry(user).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Domain.Models.User.User>> GetAllUsers()
        {
            var users = await Context.Users.ToListAsync();
            return users;
        }

        public async Task<IEnumerable<Domain.Models.User.User>> GetUsers(IEnumerable<int> ids)
        {
            var users = await Context.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
            return users;
        }
    }
}