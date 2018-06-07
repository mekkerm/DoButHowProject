using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Dbh.Model.EF.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //Returns an entity by id
        TEntity Get(int id);
        //Returns all the entities
        IEnumerable<TEntity> GetAll();
        //Returns all the entities that are fulfilling the given 
        //search criteria
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        //Returns the first entity that fulfills the given 
        //search criteria or returns null
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        //Inserts the given entity 
        void Add(TEntity entity);
        //Inserts the given list of entities
        void AddRange(IEnumerable<TEntity> entities);
        //Removes the given entity
        void Remove(TEntity entity);
        //Removes the given list of entities
        void RemoveRange(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> FindAllOrDefault(Expression<Func<TEntity, bool>> predicate);
    }
}
