using MessageService.IRepository;
using MessageService.IRepository.Query;
using MessageService.Repository.SQL.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MessageService.Repository.SQL
{
    public class GenericRepository<TModel> : IRepository<TModel> where TModel : class
    {
        protected DbSet<TModel> _models;

        public Type ElementType => _models.AsQueryable().ElementType;

        public Expression Expression => _models.AsQueryable().Expression;

        public IQueryProvider Provider => _models.AsQueryable().Provider;



        public GenericRepository(DbSet<TModel> models)
        {
            _models = models;

        }

        public virtual IEnumerable<TModel> Get()
        {
            return _models.AsEnumerable();
        }

        public virtual IEnumerable<TModel> Get(Expression<Func<TModel, bool>> predicate)
        {

            return _models.Where(predicate).AsEnumerable();
        }
        public virtual IQueryable<TModel> GetQueryable()
        {
            return _models.AsQueryable();
        }

        public virtual IQueryable<TModel> GetQueryable(Expression<Func<TModel, bool>> predicate)
        {

            return _models.Where(predicate);
        }

        public virtual TModel Remove(object id)
        {
            var entity = _models.Find(id);
            var entityEntry = null as TModel;
            if (entity != null)
            {
                entityEntry = RemoveEntity(entity);
            }
            return entityEntry;
        }

        public virtual TModel AddEntity(TModel entity)
        {
            var entityEntry = _models.Add(entity);
            return entityEntry.Entity;
        }

        public virtual TModel RemoveEntity(TModel entity)
        {
            var entityEntry = _models.Remove(entity);
            return entityEntry.Entity;
        }

        public virtual TModel UpdateEntity(TModel entity)
        {
            var entityEntry = _models.Update(entity);
            return entityEntry.Entity;
        }

        public virtual void AddRange(params TModel[] entities)
        {
            _models.AddRange(entities);
        }

        public virtual void AddRange(IEnumerable<TModel> entities)
        {
            _models.AddRange(entities);
        }

        public virtual void RemoveRange(params TModel[] entities)
        {
            _models.RemoveRange(entities);
        }

        public virtual void RemoveRange(IEnumerable<TModel> entities)
        {
            _models.RemoveRange(entities);
        }

        public virtual void UpdateRange(params TModel[] entities)
        {
            _models.UpdateRange(entities);
        }

        public virtual void UpdateRange(IEnumerable<TModel> entities)
        {
            _models.UpdateRange(entities);
        }

        public virtual TModel Get(params object[] keys)
        {
            return _models.Find(keys);
        }

        public void Load()
        {
            _models.Load();
        }



        IIncludable<TModel, TProperty> IRepository<TModel>.Include<TProperty>(Expression<Func<TModel, TProperty>> navigationPropertyPath)
        {
            return new Includable<TModel, TProperty>(_models.Include(navigationPropertyPath));
        }

        public IEnumerator<TModel> GetEnumerator()
        {
            return _models.AsQueryable<TModel>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _models.AsQueryable().GetEnumerator();
        }
    }

}
