using Microsoft.EntityFrameworkCore;
using Dbh.Model.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Dbh.Model.EF.Interfaces;

namespace Dbh.Model.EF.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
       where TEntity : BaseEntity
    {
        protected DbSet<TEntity> _dbSet;
        private DbContext _ctx;

        public GenericRepository(DbContext context)
        {
            _dbSet = context.Set<TEntity>();
            _ctx = context;
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }
    }
}
