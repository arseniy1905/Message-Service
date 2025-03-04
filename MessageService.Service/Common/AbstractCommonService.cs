using MessageService.IRepository.Unit;
using MessageService.IService.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace MessageService.Service.Common
{
    public abstract class AbstractCommonService : ICommonService
    {
        protected readonly IConfiguration _configuration;
        protected readonly AutoMapper.IMapper _mapper;
        protected IMainUnit mainUnit;

        public AbstractCommonService(IConfiguration configuration, IMainUnit mainUnit)
        {
            _configuration = configuration;
            var config = new AutoMapper.MapperConfiguration(MapperConfig);
            _mapper = config.CreateMapper();
            this.mainUnit = mainUnit;
        }

        
        protected abstract Action<AutoMapper.IMapperConfigurationExpression> MapperConfig { get; }

        public virtual void Dispose()
        {
            if (mainUnit != null)
            {
                mainUnit.Dispose();
            }
        }
        public int SaveChanges()
        {
            return mainUnit.SaveChanges();
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await mainUnit.SaveChangesAsync(cancellationToken);
        }
    }
}
