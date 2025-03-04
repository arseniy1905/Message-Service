using MessageService.IRepository.Unit;
using Microsoft.EntityFrameworkCore;

namespace MessageService.Repository.SQL.Unit
{
    public class BaseUnit : DbContext, IUnit
    {
        protected readonly string _connection;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection">Connection String</param>
        public BaseUnit(string connection)
        {
            _connection = connection;
            Init();
        }
        public BaseUnit(DbContextOptions options) : base(options)
        {
            Init();
        }
        //protected RepositoryQueryable _queryable;
        //public IRepositoryQueryable Queryable => _queryable;

        protected virtual void Init()
        {
            //_queryable = new RepositoryQueryable(this.Database);
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        
        public void SetUserId(int userId)
        {
            _userId = userId;
        }
        protected int _userId;
    }
}
