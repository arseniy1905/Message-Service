using MessageService.IRepository.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using System.Linq.Expressions;
namespace MessageService.Repository.SQL.Query
{
    internal class Includable<TEntity, TProperty> : IIncludable<TEntity, TProperty> where TEntity : class
    {
        private readonly IIncludableQueryable<TEntity, TProperty> includableQueryable;


        public Includable(IIncludableQueryable<TEntity, TProperty> includableQueryable)
        {
            this.includableQueryable = includableQueryable;
        }



        public Type ElementType => includableQueryable.ElementType;

        public Expression Expression => includableQueryable.Expression;

        public IQueryProvider Provider => includableQueryable.Provider;

        public IEnumerator<TEntity> GetEnumerator()
        {
            return includableQueryable.GetEnumerator();
        }
        // Allow to include another referenced property of the same entity
        // Exsample: Prize.Include(prize=>p.Status).Include(prize=>prize.Image)
        public IIncludable<TEntity, TNextProperty> Include<TNextProperty>(Expression<Func<TEntity, TNextProperty>> navigationPropertyPath)
        {
            return new Includable<TEntity, TNextProperty>(includableQueryable.Include(navigationPropertyPath));

        }
        // 




        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
