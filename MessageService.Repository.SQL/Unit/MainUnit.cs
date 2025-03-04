using MessageService.DataModel;
using MessageService.IRepository;
using MessageService.IRepository.Unit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MessageService.Repository.SQL.Unit
{
    public class MainUnit : BaseUnit, IMainUnit
    {
        protected override void Init()
        {
            base.Init();
            #region repositories
            _settings = new GenericRepository<Setting>(Set<Setting>());
            _messages=new GenericRepository<Message>(Set<Message>());
            #endregion repositories
        }

        public MainUnit(string connection) : base(connection)
        {


        }

        public MainUnit(DbContextOptions options) : base(options)
        {

        }

        #region generics

                                 
        private GenericRepository<Setting> _settings;
        public IRepository<Setting> Settings => _settings;
               
        private GenericRepository<Message> _messages;
        public IRepository<Message> Messages => _messages;

        #endregion generics

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connection);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().HasKey(m=>m.PhoneNumber);
                      
            modelBuilder.Entity<Setting>().HasKey(s=>s.KeyName);
            
            


        }
        public bool EnsureDeleted()
        {
            return Database.EnsureDeleted();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
