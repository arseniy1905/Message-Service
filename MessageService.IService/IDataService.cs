using System;

namespace MessageService.IService
{
    public interface IDataService : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
