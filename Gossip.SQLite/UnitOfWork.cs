using Gossip.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Gossip.SQLite
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GossipContext _dbContext;
        private readonly IDbContextTransaction _dbTransaction;
        private readonly IMediator _mediator;
        private bool _commited = false;

        public UnitOfWork(GossipContext dbContext, IDbContextTransaction dbTransaction, IMediator mediator)
        {
            _dbContext = dbContext;
            _dbTransaction = dbTransaction;
            _mediator = mediator;
        }

        public void Dispose()
        {
            if (!_commited) _dbTransaction.Rollback();
        }

        public async Task CommitChangesAsync()
        {
            await _mediator.DispatchDomainEventsAsync(_dbContext);
            _dbTransaction.Commit();
            _commited = true;
        }
    }

    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly GossipContext _dbContext;
        private readonly IMediator _mediator;

        public UnitOfWorkFactory(GossipContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<IUnitOfWork> CreateAsync()
        {
            var dbTransaction = await _dbContext.Database.BeginTransactionAsync();
            return new UnitOfWork(_dbContext, dbTransaction, _mediator);
        }
    }
}
