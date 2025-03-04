using System;
using System.Linq;
using System.Linq.Expressions;

namespace MessageService.IRepository.Query
{
    public interface IIncludable<TModel, TProperty> : IQueryable<TModel>
    {

        IIncludable<TModel, TNextProperty> Include<TNextProperty>(Expression<Func<TModel, TNextProperty>> navigationPropertyPath);


    }
}
