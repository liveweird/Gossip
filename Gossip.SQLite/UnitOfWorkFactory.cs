using System.Threading.Tasks;
using Gossip.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gossip.SQLite
{
    public class UnitOfWorkFactory<TContext> : IUnitOfWorkFactory where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private readonly IMediator _mediator;

        public UnitOfWorkFactory(TContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<IUnitOfWork> CreateAsync()
        {
            var dbTransaction = await _dbContext.Database.BeginTransactionAsync();
            return new UnitOfWork<TContext>(_dbContext, dbTransaction, _mediator);
        }
    }
}