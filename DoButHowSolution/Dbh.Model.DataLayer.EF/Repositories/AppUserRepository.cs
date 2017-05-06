using Dbh.Model.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Dbh.Model.EF.Interfaces;
using Dbh.Model.EF.Interfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Dbh.Model.EF.Repositories
{
    public class AppUserRepository : IRepository<ApplicationUser>, IAppUserRepository
    {
        private DbSet<ApplicationUser> _dbSet;
        private DbContext _ctx;

        public AppUserRepository(DbContext context)
        {
            _dbSet = context.Set<ApplicationUser>();
            _ctx = context;
        }

        public void Add(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<ApplicationUser> entities)
        {
            _dbSet.AddRange(entities);
        }

        public IEnumerable<ApplicationUser> Find(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public ApplicationUser Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Remove(ApplicationUser entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<ApplicationUser> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public ApplicationUser SingleOrDefault(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public ApplicationUser GetUser(string id)
        {
            return _dbSet.Find(id);
        }
    }

}
