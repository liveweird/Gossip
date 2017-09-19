using Gossip.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gossip.SQLite
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private readonly IDbContextTransaction _dbTransaction;
        private readonly IMediator _mediator;
        private bool _commited = false;

        public UnitOfWork(TContext dbContext, IDbContextTransaction dbTransaction, IMediator mediator)
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
            _dbContext.SaveChanges();
            _dbTransaction.Commit();
            _commited = true;
        }
    }
}
