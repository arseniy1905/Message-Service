using MessageService.IRepository.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MessageService.IRepository
{
    public interface IRepository<TModel> : IQueryable<TModel> where TModel : class
    {
        TModel Get(params object[] keys);
        IEnumerable<TModel> Get();
        IEnumerable<TModel> Get(Expression<Func<TModel, bool>> predicate);
        IQueryable<TModel> GetQueryable();
        IQueryable<TModel> GetQueryable(Expression<Func<TModel, bool>> predicate);
        TModel AddEntity(TModel entity);
        void AddRange(params TModel[] entities);
        void AddRange(IEnumerable<TModel> entities);


        TModel RemoveEntity(TModel entity);
        TModel Remove(object id);
        void RemoveRange(params TModel[] entities);
        void RemoveRange(IEnumerable<TModel> entities);


        TModel UpdateEntity(TModel entity);
        void UpdateRange(params TModel[] entities);
        void UpdateRange(IEnumerable<TModel> entities);
        void Load();
        IIncludable<TModel, TProperty> Include<TProperty>(Expression<Func<TModel, TProperty>> navigationPropertyPath);
        


    }
}
