using AutoMapper;
using MessageService.IRepository.Unit;
using MessageService.IService;
using System;

namespace MessageService.Service
{
    public abstract class AbstractDataService<TUnit> : IDataService where TUnit : IUnit
    {
        protected readonly IMainUnit _unit;
        protected readonly IMapper _mapper;
        protected AbstractDataService(IMainUnit unit)
        {
            _unit = unit;
            var config = new MapperConfiguration(MapperConfig);
            _mapper = config.CreateMapper();
        }

        protected abstract Action<IMapperConfigurationExpression> MapperConfig { get; }

        public virtual void Dispose()
        {
            if (_unit != null)
            {
                _unit.Dispose();
            }

        }

        public int SaveChanges()
        {
            return _unit.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _unit.SaveChangesAsync(cancellationToken);
        }
    }
}
