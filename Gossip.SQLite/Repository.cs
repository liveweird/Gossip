using System;
using System.Collections.Generic;
using System.Linq;
using Gossip.Domain;
using Gossip.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gossip.SQLite
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly GossipContext _context;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private DbSet<T> _entities;

        protected Repository(GossipContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public T Get(int id)
        {
            return _entities.SingleOrDefault(p => p.Id == id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}